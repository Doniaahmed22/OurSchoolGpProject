using School.Data.Entities;
using School.Data.Entities.ChatHub;
using School.Data.Entities.Identity;
using School.Repository.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Interfaces
{
    public interface IChatRepository:IGenericRepository<Message>
    {
        Task<IEnumerable<ChatWithLastMessage>> GetRecentChatsForUser(String userId);
        Task<IEnumerable<Message>> GetChatBetweenTowUser(string UserId1, string UserId2);
        Task<List<(AppUser user, string roleName)>> FindFriendsByName(string userid, string Name);
        Task<List<(AppUser user, string roleName)>> GetAllChatFriends(string userid);


    }
}
