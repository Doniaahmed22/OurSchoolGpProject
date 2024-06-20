using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Services.Services.DepartmentService;
using School.Services.Services.LevelService;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelController : ControllerBase
    {
        readonly private ILevelService _LevelService;
        public LevelController(ILevelService levelService)
        {
            _LevelService = levelService;
        }
        [HttpGet("GetAllLevelsForList")]
        public async Task<IActionResult> GetAllLevelsForList()
        {
            var levels = await _LevelService.GetAllLevelsForList();
            return Ok(levels);
        }
    }
}
