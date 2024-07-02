using Microsoft.AspNetCore.SignalR;
using School.Data.Entities.ChatHub;
using School.Repository.Interfaces.ChatHub;
using School.Services.Services.ChatService;
using School.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Hubs
{
    public class ChatHub :Hub
    {
        private readonly IChatService _chatService;
        private readonly IActiveUserRepository _activeUserRepository;
//        private readonly IUserService _userService;

        public ChatHub (IChatService chatService ,IUserService userService, IActiveUserRepository activeUserRepository)
        {
            _chatService = chatService;
            _activeUserRepository = activeUserRepository;
 //           _userService = userService;
        }
        public async Task SendMessageToUser(string senderid,string recieverid, string message)
        {
            var Storedmessage = await _chatService.StoreMessage(senderid,recieverid,message);
            ActiveUserConnection activeUser= await _activeUserRepository.GetActiveUserByUserId(recieverid);
            if (activeUser!=null)
            {/*
                var username = authenticatedUserService.GetAuthenticatedUsername();
                await Clients.Client(activeUser.ConnectionId).SendAsync("ReceiveMessage", Storedmessage, username);*/
                await Clients.Client(activeUser.ConnectionId).SendAsync("ReceiveMessage", Storedmessage);

            }
        }
 

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
/*
        public List<int> GetActiveUserIds()
        {
            return activeUsers.Keys.ToList();
        }
*/
        public override async Task OnConnectedAsync()
        {
            var connectionId = GetConnectionId();
            // var userId = _userService.GetAuthenticatedUserId();
            var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _activeUserRepository.AddActiveUser(userId, connectionId);  
            //await Clients.All.SendAsync("ReceiveActiveUsers", GetActiveUserIds());
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = GetConnectionId();
            await _activeUserRepository.RemoveActiveUser(connectionId);
            //await Clients.All.SendAsync("ReceiveActiveUsers", GetActiveUserIds());
            await base.OnDisconnectedAsync(exception);
        }
    }
}
