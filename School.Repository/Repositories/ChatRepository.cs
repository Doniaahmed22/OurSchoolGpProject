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
using System.Security.Cryptography;
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
            List<ChatWithLastMessage>usersWithLastMessage = new List<ChatWithLastMessage>();
            var chatIdsQuery = _context.Messages
                .Where(o => o.SenderId.Equals(userId) || o.ReceiverId.Equals(userId))
                .Select(x => new
                {
                    x.Id,
                    x.ReceiverId,
                    x.SenderId
                })
                .GroupBy(g => new { g.SenderId, g.ReceiverId })
                .Select(k => new
                {
                    Id = k.Max(x => x.Id),
                    PartnerId = k.Key.SenderId.Equals(userId) ? k.Key.ReceiverId : k.Key.SenderId,
                    //CreateTime = k.Max(x => x.CreateTime)
                })
                .OrderByDescending(d => d.Id)
                .AsQueryable();


            var chatListQuery = from l in chatIdsQuery
                                join x in _context.Messages on l.Id equals x.Id
                                orderby x.MessageDate descending
                                select (new
                                {
                                    x.Id,
                                    l.PartnerId,
                                    x.Content,
                                    x.MessageDate ,
                                    x.SenderId ,
                                    x.ReceiverId
                                });
            var chatList = await chatListQuery.ToListAsync();
            chatList = chatList.DistinctBy(i => i.PartnerId).ToList();

            foreach (var chat in chatList)
            {
                ChatWithLastMessage userWithLast = new ChatWithLastMessage();
                userWithLast.User = await _userManager.FindByIdAsync(chat.PartnerId);
                userWithLast.LastMessage = new Message
                {
                    Content = chat.Content,
                    Id = chat.Id,
                    MessageDate = chat.MessageDate,
                    ReceiverId = chat.ReceiverId,
                    SenderId = chat.SenderId,
                };
                usersWithLastMessage.Add(userWithLast);
            }
            return usersWithLastMessage;
        }
        public async Task<IEnumerable<Message>> GetChatBetweenTowUser(string UserId1, string UserId2)
        {
            return await _context.Messages.Where(m => (m.SenderId.Equals( UserId1) && m.ReceiverId.Equals(UserId2)) ||
                   (m.SenderId.Equals(UserId2) && m.ReceiverId.Equals(UserId1)))
                    .OrderBy(m => m.MessageDate).ToListAsync();
        }

        public async Task<List<(AppUser user, string roleName)>> FindFriendsByName( string userid, string Name)
        {
            var usersWithRoles = new List<(AppUser user, string roleName)>();

            var currnt_user = await _userManager.FindByIdAsync(userid);
            var UserRole = await _userManager.GetRolesAsync(currnt_user);

            var users = await _userManager.Users
                        .Where(u => u.DisplayName.Contains(Name) && u.Id != userid)
                        .OrderBy(u=>u.DisplayName)
                        .ToListAsync();

            if (UserRole.Contains("Teacher"))
            {
                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        if (role == "Admin" || role == "Teacher")
                            continue;
                        usersWithRoles.Add((user, role));
                    }
                }
            }
            else if (UserRole.Contains("Student"))
            {
                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        if (role == "Admin" || role == "Parent")
                            continue;
                        usersWithRoles.Add((user, role));
                    }
                }
            }
            else if (UserRole.Contains("Parent"))
            {
                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        if (role == "Admin" || role == "Parent" || role == "Student")
                            continue;
                        usersWithRoles.Add((user, role));
                    }
                }
            }
            return usersWithRoles;

        }
        
       public async Task<List<(AppUser user, string roleName)>> GetAllChatFriends(string userId)
        {
            var usersWithRoles = new List<(AppUser user, string roleName)>();

            var chatIdsQuery = _context.Messages
               .Where(o => o.SenderId.Equals(userId) || o.ReceiverId.Equals(userId))
               .Select(x => new
               {
                   x.Id,
                   x.ReceiverId,
                   x.SenderId
               })
               .GroupBy(g => new { g.SenderId, g.ReceiverId })
               .Select(k => new
               {
                   Id = k.Max(x => x.Id),
                   PartnerId = k.Key.SenderId.Equals(userId) ? k.Key.ReceiverId : k.Key.SenderId,
                   //CreateTime = k.Max(x => x.CreateTime)
               })
               .OrderByDescending(d => d.Id)
               .AsQueryable();
                 var chatList = await chatIdsQuery.ToListAsync();
                 chatList = chatList.DistinctBy(i => i.PartnerId).ToList();

            if (chatList.Count() > 0)
            {

                foreach (var chat in chatList)
                {
                    AppUser User = await _userManager.FindByIdAsync(chat.PartnerId);
                    var roles = await _userManager.GetRolesAsync(User);
                    usersWithRoles.Add((User, roles.FirstOrDefault()));

                }
                return usersWithRoles;
            }

            var currnt_user   = await _userManager.FindByIdAsync(userId);
            var UserRole =await _userManager.GetRolesAsync(currnt_user);
            var users = await _userManager.Users
                        .Where(u => u.Id != userId)
                        .OrderBy(u => u.DisplayName)
                        .ToListAsync();

            if (UserRole.Contains("Teacher")){
                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        if (role == "Admin"||role=="Teacher")
                            continue;
                        usersWithRoles.Add((user, role));
                    }
                }
            }
            else if (UserRole.Contains("Student"))
            {
                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        if (role == "Admin"||role == "Parent")
                            continue;
                        usersWithRoles.Add((user, role));
                    }
                }
            }
            else if (UserRole.Contains("Parent"))
            {
                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        if (role == "Admin" || role == "Parent"||role=="Student")
                            continue;
                        usersWithRoles.Add((user, role));
                    }
                }
            }
            return usersWithRoles;
        }
    }
}
