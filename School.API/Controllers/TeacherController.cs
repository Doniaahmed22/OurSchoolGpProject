using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Services.Dtos.SubjectDto;
using School.Services.Dtos.TeacherDto;
using School.Services.Services.SubjectServices;
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

        [HttpGet("SearchForTeacher/{name:alpha}")]
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
