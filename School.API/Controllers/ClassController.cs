using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Data.Context;
using School.Data.Entities;
/*
namespace School.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public ClassController(SchoolDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetClasses")]
        public async Task<IActionResult> GetClasses()
        {
            var classes = await _context.Classes.ToListAsync();
            return Ok(classes);
        }

        [HttpGet]
        [Route("GetClassById/{id}")]
        public async Task<IActionResult> GetClassById(int id)
        {
            var classItem = await _context.Classes.FindAsync(id);
            if (classItem == null)
            {
                return NotFound();
            }
            return Ok(classItem);
        }

        [HttpPost]
        [Route("AddClass")]
        public async Task<IActionResult> AddClass(Class classItem)
        {
            _context.Classes.Add(classItem);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Route("UpdateClass")]
        public async Task<IActionResult> UpdateClass(Class classItem)
        {
            if (classItem == null || classItem.Id == 0)
            {
                return BadRequest();
            }

            var existingClass = await _context.Classes.FindAsync(classItem.Id);
            if (existingClass == null)
            {
                return NotFound();
            }

            existingClass.Name = classItem.Name;
            existingClass.NumOfStudent = classItem.NumOfStudent;
            existingClass.DepartmentId = classItem.DepartmentId;
            existingClass.LevelId = classItem.LevelId;

            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete]
        [Route("DeleteClass/{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var classItem = await _context.Classes.FindAsync(id);
            if (classItem == null)
            {
                return NotFound();
            }

            _context.Classes.Remove(classItem);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
*/