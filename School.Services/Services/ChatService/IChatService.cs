using School.Services.Dtos.ChatDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.ChatService
{
    public interface IChatService
    {
        Task<IEnumerable<ChatWithLastMessageDto>> GetRecentChatsForUser(String userId);
        Task<IEnumerable<MessageDto>> GetChatBetweenTowUser(string UserId1, string UserId2);
        Task<MessageDto> StoreMessage(string Senderid, string recieverid, string message);

    }
}
