using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Services.Services.DepartmentService;
using School.Services.Services.TermService;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermController : ControllerBase
    {
        readonly private ITermService _termService;
        public TermController(ITermService termService)
        {
            _termService = termService;
        }
        [HttpGet("GetAllTermsForList")]
        public async Task<IActionResult> GetAllTermsForList()
        {
            var Terms = await _termService.GetAllTermsForList();
            return Ok(Terms);
        }
    }
}
