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
        [HttpGet("GetRecentChatsForUser/{UserId:alpha}")]
        public async Task<IActionResult> GetRecentChatsForUser(string UserId)
        {
            var ChatsForUser = await _chatService.GetRecentChatsForUser(UserId);
            return Ok(ChatsForUser);
        }
        [HttpGet("GetChatBetweenTowUser/{UserId1:alpha}/{UserId2:alpha}")]
        public async Task<IActionResult> GetChatBetweenTowUser(string UserId1 , string UserId2)
        {
            var Chat = await _chatService.GetChatBetweenTowUser(UserId1,UserId2);
            return Ok(Chat);
        }
    }
}
