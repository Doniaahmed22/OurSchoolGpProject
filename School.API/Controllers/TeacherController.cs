using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Data.Context;
using School.Data.Entities.Identity;
using School.Services.Dtos.ParentDto;
using School.Services.Dtos.StudentDto;
using School.Services.Dtos.TeacherDto;
using School.Services.Services.TeacherServices;
using School.Services.UserService;
using School.Services.UserService.Dtos;
using System.Drawing;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherServices TeacherServices;
        private readonly SchoolDbContext _context;
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public TeacherController(ITeacherServices teacherServices, SchoolDbContext context, IUserService userService, UserManager<AppUser> userManager)
        {
            TeacherServices = teacherServices;
            _context = context;
            _userService = userService;
            _userManager = userManager;
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

            RegisterDto registerDto = new RegisterDto
            {
                DisplayName = teacherDto.Name,
                GmailAddress = teacherDto.GmailAddress,
                Email = teacherDto.Name.Split(" ")[0]+ teacherDto.Name.Split(" ")[1] + teacherDto.BirthDay.Day + teacherDto.BirthDay.Month + teacherDto.BirthDay.Year + "@gmail.com",
                Password = teacherDto.Name.Split(" ")[0].ToUpper() + teacherDto.Name.Split(" ")[1].ToLower() + teacherDto.BirthDay.Day + teacherDto.BirthDay.Month + teacherDto.BirthDay.Year + "!",
            };
            _userService.Register(registerDto,"Teacher");

            teacherDto.Email = registerDto.Email;
            await TeacherServices.AddTeacher(teacherDto);
            return Ok(teacherDto);
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

            var teach = await _context.Teachers.FindAsync(id);

            var user = await _userManager.FindByEmailAsync(teach.Email);

            if (user is null)
            {
                throw new Exception("User Email not found");
            }

            _userManager.DeleteAsync(user);

            var teacher = await TeacherServices.DeleteTeacher(id);
            if (teacher == null)
                return NotFound();
            return Ok();
        }


    }
}
