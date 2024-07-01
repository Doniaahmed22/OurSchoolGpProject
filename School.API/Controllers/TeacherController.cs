using Microsoft.AspNetCore.Http;
using School.Services.Dtos.SubjectDto;
using School.Services.Services.SubjectServices;
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
using Microsoft.AspNetCore.Authorization;

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
        //[Authorize(Roles ="Teacher")]
        public async Task<IActionResult> GetAllTeacher() 
        {
            var teachers = await TeacherServices.GetTeachers();
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


        [HttpGet("SearchForTeacher/{name}")]
        public async Task<IActionResult> GetTeachersByName(string name)
        {
            var teachers = await TeacherServices.GetTeachers(name);
            return Ok(teachers);
        }
        
        
        [HttpGet("GetTeacherSubjects/{Teacherid:int}")]  //int teacher mode subject page =>retuen all subject in level that teacher teach
        public IActionResult GetTeacherSubjects(int Teacherid)
        {
            var subject = TeacherServices.GetTeacherSubjectsInLevel(Teacherid);
            if (subject == null)
                return NotFound();
            return Ok(subject);
        }


        [HttpGet("GetTeacherLevel/{Teacherid:int}")]  //int teacher mode subject page =>GetAllLevelThatTeache teach in
        public IActionResult GetTeacherLevel(int Teacherid)
        {
            var Levels = TeacherServices.GetTeacherLevels(Teacherid);
            if (Levels == null)
                return NotFound();
            return Ok(Levels);
        }


        [HttpGet("GetTeacherSubjectsInLevel/{Teacherid:int}/{Levelid:int}")]  //in teacher mode subject page =>GetAll subject in  specific level teach 
        public IActionResult GetTeacherSubjectsInLevel(int Teacherid,int Levelid)
        {
            var subjects = TeacherServices.GetTeacherSubjectsInLevel(Teacherid,Levelid);
            if (subjects == null)
                return NotFound();
            return Ok(subjects);
        }
       

        [HttpGet("GetTeacherClassesByLevelSubject/{Teacherid:int}")]  //in teacher mode material page =>GetAll classes in  specific level and subject teach 
        public async Task< IActionResult> GetTeacherClassesByLevelSubject(int Teacherid, int Levelid,int subjectid)
        {
            var classes = await TeacherServices.GetTeacherClassesByLevelSubAsync (Teacherid, Levelid, subjectid);
            if (classes == null)
                return NotFound();
            return Ok(classes);
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
                Email = teacherDto.Name.Split(" ")[0]+ teacherDto.Name.Split(" ")[1] + teacherDto.BirthDay.Day + teacherDto.BirthDay.Month + teacherDto.BirthDay.Year + "@school.com",
                Password = teacherDto.Name.Split(" ")[0].ToUpper() + teacherDto.Name.Split(" ")[1].ToLower() + teacherDto.BirthDay.Day + teacherDto.BirthDay.Month + teacherDto.BirthDay.Year + "!",
            };
            await _userService.Register(registerDto,"Teacher");

            teacherDto.Email = registerDto.Email;
            await TeacherServices.AddTeacher(teacherDto);
            await _userService.SendEmail(registerDto);
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
