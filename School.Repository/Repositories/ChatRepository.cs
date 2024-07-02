using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using School.Data.Context;
using School.Data.Entities;
using School.Data.Entities.ChatHub;
using School.Data.Entities.Identity;
using School.Repository.Dto;
using School.Repository.Interfaces;
using School.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace School.Repository.Repositories
{
    public class ChatRepository : GenericRepository<Message>, IChatRepository
    {
        private readonly UserManager<AppUser> _userManager;
        public ChatRepository(SchoolDbContext context, UserManager<AppUser> userManager) : base(context)
        {
            _userManager = userManager;
        }
        public async Task<IEnumerable<ChatWithLastMessage>> GetRecentChatsForUser(String userId)
        {
            List<ChatWithLastMessage> usersWithLastMessage = new List<ChatWithLastMessage>();
            var Conversions = await _context.Messages
                .Where(m => m.SenderId == userId || m.ReceiverId == userId).ToListAsync();

           var Conversions2 = Conversions.GroupBy(m => m.SenderId == userId ? m.ReceiverId : m.SenderId)
                .OrderByDescending(g => g.Max(m => m.MessageDate))
                .Take(10);

            foreach (var conversion in Conversions2)
            {
                AppUser User = await _userManager.FindByIdAsync(conversion.Key);
                Message LastMessage = conversion.OrderByDescending(msg => msg.MessageDate).First();
                usersWithLastMessage.Add(new ChatWithLastMessage() { User = User, LastMessage = LastMessage });
            }

            return usersWithLastMessage;
        }
        public async Task<IEnumerable<Message>> GetChatBetweenTowUser(string UserId1, string UserId2)
        {
            return await _context.Messages.Where(m => (m.SenderId == UserId1 && m.ReceiverId == UserId2) ||
                   (m.SenderId == UserId2 && m.ReceiverId == UserId1))
                    .OrderBy(m => m.MessageDate).ToListAsync();
        }

        public async Task<List<(AppUser user, string roleName)>> FindFriendsByName( string userid, string Name)
        {
            var users = await _userManager.Users
                        .Where(u => u.DisplayName.Contains(Name) && u.Id != userid).Take(10)
                        .OrderBy(u=>u.DisplayName)
                        .ToListAsync();
            var usersWithRoles = new List<(AppUser user, string roleName)>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user) ;
                foreach (var role in roles)
                {
                    usersWithRoles.Add((user, role));
                }
            }

            return usersWithRoles;
        }
        
       public async Task<List<(AppUser user, string roleName)>> GetAllChatFriends(string userid)
        {
            var users = await _userManager.Users
                        .Where(u => u.Id != userid).Take(10)
                        .OrderBy(u => u.DisplayName)
                        .ToListAsync();
            var usersWithRoles = new List<(AppUser user, string roleName)>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    usersWithRoles.Add((user, role));
                }
            }

            return usersWithRoles;
        }
    }
}
