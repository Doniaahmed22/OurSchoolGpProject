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
                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .GroupBy(m => m.SenderId == userId ? m.ReceiverId : m.SenderId)
                .OrderByDescending(g => g.Max(m => m.MessageDate))
                .Take(10).ToListAsync();

            foreach (var conversion in Conversions)
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

        public async Task<List<(AppUser user, string roleName)>> FindFriendsByName(string Name)
        {
            var usersWithRoles = await _userManager.Users
                .SelectMany(
                    user => _userManager.GetRolesAsync(user).Result.DefaultIfEmpty(),
                    (user, roleName) => new { User = user, RoleName = roleName }
                )
                .Where(ur => ur.RoleName != null&&ur.User.DisplayName.Contains(Name))
                .Select(ur => new { User = ur.User, RoleName = ur.RoleName })
                .ToListAsync();
            var result = usersWithRoles.Select(ur => (ur.User, ur.RoleName)).ToList();

            return result;
        }
        
       public async Task<List<(AppUser user, string roleName)>> GetAllFriends()
        {
            var usersWithRoles = await _userManager.Users
                .SelectMany(
                    user => _userManager.GetRolesAsync(user).Result.DefaultIfEmpty(),
                    (user, roleName) => new { User = user, RoleName = roleName }
                )
                .Where(ur => ur.RoleName != null)//notknow if we will include admin in chat or not
                .Select(ur => new { User = ur.User, RoleName = ur.RoleName })
                .ToListAsync();
            var result = usersWithRoles.Select(ur => (ur.User, ur.RoleName)).ToList();

            return result;
        }
    }
}
