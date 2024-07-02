using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.ChatDto
{
    public class ChatWithLastMessageDto
    {
        public FriendChatDto Friend { get; set; } 

       public MessageDto LastMessage { get; set; }

    }
}
