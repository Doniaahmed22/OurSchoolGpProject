using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Services.Dtos.TeacherDto;
using School.Services.Services.TeacherServices;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherServices TeacherServices;

        public TeacherController(ITeacherServices teacherServices)
        {
            TeacherServices = teacherServices;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllTeacher() 
        {
            var teachers = await TeacherServices.GetAll();
            return Ok(teachers);
        }
        [HttpGet("GetById/{id:int}")]
        public async Task<IActionResult> GetTeacherById(int id)// ??teacher level- subjects and level - subjects and class
        {
            var teacher = await TeacherServices.GetTeacherById(id);
            if(teacher == null) 
                return NotFound();
            return Ok(teacher);
        }

        [HttpGet("SearchForTeacher/{name:alpha}")]
        public async Task<IActionResult> GetTeachersByName(string name)
        {
            var teachers = await TeacherServices.GetTeachers(name);
            return Ok(teachers);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddTeacher(AddTeacherDto teacherDto)
        {
            if(teacherDto == null)
                return BadRequest("Teacher is empty");
            await TeacherServices.AddTeacher(teacherDto);
            return Ok();
        }

        [HttpPut("Update/{id:int}")]
        public async Task<IActionResult> UpdateTeacher(int id , AddTeacherDto teacherDto)
        {
            if (teacherDto == null)
                return BadRequest("Teacher is empty");
            var teacher = await TeacherServices.UpdateTeacher(id, teacherDto);
            if (teacher == null)
                return NotFound();
            return Ok();
        }



        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await TeacherServices.DeleteTeacher(id);
            if (teacher == null)
                return NotFound();
            return Ok();
        }


    }
}
