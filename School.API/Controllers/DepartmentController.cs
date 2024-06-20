using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Services.Services.DepartmentService;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        readonly private IdepartmentService _departmentService;
        public DepartmentController(IdepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet("GetAllDepartmentsForList")]
        public async Task <IActionResult > GetAllDepartmentsForList() { 
            var depts= await _departmentService.GetAllDepartmentForList();
            return Ok(depts);
        }
    }
}
