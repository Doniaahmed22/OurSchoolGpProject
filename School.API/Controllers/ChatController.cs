using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Services.Services.ChatService;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
        [HttpGet("GetRecentChatsForUser/{UserId}")]
        public async Task<IActionResult> GetRecentChatsForUser(string UserId)
        {
            var ChatsForUser = await _chatService.GetRecentChatsForUser(UserId);
            return Ok(ChatsForUser);
        }
        [HttpGet("GetChatBetweenTowUser/{UserId1}/{UserId2}")]
        public async Task<IActionResult> GetChatBetweenTowUser(string UserId1, string UserId2)
        {
            var Chat = await _chatService.GetChatBetweenTowUser(UserId1, UserId2);
            return Ok(Chat);
        }
        [HttpGet("GetAllFriends/{UserId}")]
        public async Task<IActionResult> GetAllFriends(string UserId)
        {
            var friends = await _chatService.FindFriends(UserId);
            return Ok(friends);
        }

        [HttpGet("SearchForUser/{UserId}/{Name}")]
        public async Task<IActionResult> SearchForUser(string UserId, string Name)
        {
            var friends = await _chatService.FindFriends(UserId, Name);
            return Ok(friends);
        }
        [HttpPost("SendMessage/{SenderId}/{ReciverId}/{Message}")]
        public async Task<IActionResult> SendMessage(string SenderId, string ReciverId,string Message)
        {
            var Chat = await _chatService.StoreMessage(SenderId, ReciverId , Message);
            return Ok(Chat);
        }

    }
}
