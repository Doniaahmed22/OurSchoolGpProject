using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Services.Services.RequestMeetingService;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestMeetingController : ControllerBase
    {
        private readonly IRequestMeetingService _requestMeetingService;
        public RequestMeetingController(IRequestMeetingService requestMeetingService) {
            _requestMeetingService = requestMeetingService;
        }
        [HttpPost("AddRequestMeeting/{StudentId:int}")]
        public async Task <IActionResult> AddRequestMeeting(int StudentId) { 
            await  _requestMeetingService.AddRequestMeeting(StudentId);
            return Ok();
        }
        [HttpGet("GetRequestMeetingDates/{StudentId:int}")]
        public async Task<IActionResult> GetRequestMeetingDates(int StudentId)
        {
           var dto = await _requestMeetingService.GetRequestMeetingDates(StudentId);
            return Ok(dto);
        }
    }
}
