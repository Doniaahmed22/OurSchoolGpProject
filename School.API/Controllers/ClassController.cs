using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Data.Context;
using School.Data.Entities;
using School.Services.Dtos.ClassDto;
using School.Services.Services.ClassServices;

namespace School.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly SchoolDbContext _context;
        private readonly IClassServices classServices;
        public ClassController(IClassServices classServices,SchoolDbContext context)
        {
            this.classServices = classServices;
            _context = context;
        }

        [HttpGet]
        [Route("GetClasses")]
        public IActionResult GetAllClasses()
        {
            var classes = classServices.GetAllClasses();
            return Ok(classes);
        }

        [HttpGet]
        [Route("GetClassById/{id}")]
        public async Task<IActionResult> GetClassById(int id)
        {
            var classItem = await classServices.GetClassById(id);
            if (classItem == null)
            {
                return NotFound();
            }
            return Ok(classItem);
        }
        [HttpGet("TeachersSubject/{id:int}")]
        public async Task<IActionResult> GetClassTeacherSubject(int id)
        {
            var classItem = await classServices.GetClassTeacherSubject(id);
            if (classItem == null)
            {
                return NotFound();
            }
            return Ok(classItem);

        }
        [HttpPost]
        [Route("AddClass")]
        public async Task<IActionResult> AddClass(ClassAddUpdateDto classItem)
        {
            await classServices.AddClass(classItem);
            return Ok();
        }

        [HttpPut]
        [Route("UpdateClass/{id:int}")]
        public async Task<IActionResult> UpdateClass(int id,ClassAddUpdateDto classItem)
        {
            if (classItem == null || id == 0)
            {
                return BadRequest();
            }

            var existingClass = await classServices.UpdateClass(id,classItem);
            if (existingClass == null)
            {
                return NotFound();
            }
            return Ok();
        }


        [HttpDelete]
        [Route("DeleteClass/{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var classItem = await classServices.DeleteClass(id);
            if (classItem == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
