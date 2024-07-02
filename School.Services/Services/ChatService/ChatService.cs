using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using School.Data.Entities;
using School.Data.Entities.ChatHub;
using School.Data.Entities.Identity;
using School.Repository.Dto;
using School.Repository.Interfaces;
using School.Services.Dtos.ChatDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.ChatService
{
    public class ChatService:IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly UserManager<AppUser> _userManager;
        public ChatService(IChatRepository chatRepository, UserManager<AppUser> userManager)
        {
            _chatRepository = chatRepository;
            _userManager = userManager;
        }
        public async Task<IEnumerable<ChatWithLastMessageDto>> GetRecentChatsForUser(String userId)
        {
            List<ChatWithLastMessageDto> chatsUserDto = new List<ChatWithLastMessageDto>();
            IEnumerable<ChatWithLastMessage> chats = await _chatRepository.GetRecentChatsForUser(userId);
            foreach (var chat in chats)
            {
                ChatWithLastMessageDto chatDto = new ChatWithLastMessageDto();
                chatDto.Friend = new FriendChatDto();

                chatDto.Friend.UserId = chat.User.Id;
                chatDto.Friend.UserName = chat.User.DisplayName;
                var Roles = await _userManager.GetRolesAsync(chat.User);
                chatDto.Friend.Role = Roles.FirstOrDefault();
                chatDto.LastMessage = new MessageDto()
                {
                    Content = chat.LastMessage.Content,
                    Id = chat.LastMessage.Id,
                    MessageDate = chat.LastMessage.MessageDate,
                    ReceiverId = chat.LastMessage.ReceiverId,
                    SenderId = chat.LastMessage.SenderId
                };
                chatsUserDto.Add(chatDto);
            }
            return chatsUserDto;

        }
        public async Task<IEnumerable<MessageDto>> GetChatBetweenTowUser(string UserId1, string UserId2)
        {
            List<MessageDto> messagesDto = new List<MessageDto>();
            IEnumerable<Message> messages = await _chatRepository.GetChatBetweenTowUser(UserId1, UserId2);
            foreach (var message in messages)
            {
                messagesDto.Add(new MessageDto()
                {
                    Content = message.Content,
                    Id = message.Id,
                    MessageDate = message.MessageDate,
                    ReceiverId = message.ReceiverId,
                    SenderId = message.SenderId
                });
            }
            return messagesDto;
        }
        public async Task<MessageDto> StoreMessage(string Senderid, string recieverid, string message)
        {
            Message mess = new Message()
            {
                Content = message,
                MessageDate = DateTime.Now,
                ReceiverId = recieverid,
                SenderId = Senderid
            };
            await _chatRepository.Add(mess);
            MessageDto messageDto = new MessageDto()
            {
                Content = message,
                Id = mess.Id,
                MessageDate = mess.MessageDate,
                ReceiverId = mess.ReceiverId,
                SenderId = mess.SenderId

            };
            return messageDto;
        }

        public async Task<IEnumerable< FriendChatDto>> FindFriends(string userid,string Name = null)
        {
            List<FriendChatDto>friendsChatDto = new List<FriendChatDto>();
            IEnumerable<(AppUser user, string roleName)> friends;
            if(Name != null)
                friends = await _chatRepository.FindFriendsByName( userid,Name);
            else
                friends = await _chatRepository.GetAllChatFriends( userid);

            foreach (var friend in friends)
            {
                friendsChatDto.Add(new FriendChatDto()
                {
                    Role = friend.roleName,
                    UserId = friend.user.Id,
                    UserName = friend.user.DisplayName
                });
            }
            return friendsChatDto;
        }

    }
}
