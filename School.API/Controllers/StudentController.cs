using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Data.Context;
using School.Services.Dtos.GradesDto;
using School.Services.Dtos.StudentDto;
using School.Services.Dtos.SubjectDto;
using School.Services.Services.StudentServices;

namespace School.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentServices _studentServices;
        private readonly SchoolDbContext _context;

        public StudentController(IStudentServices studentServices, SchoolDbContext context)
        {
            _studentServices = studentServices;
            _context = context;
        }

        [HttpGet]
        [Route("GetStudents")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
        {
            var students = await _studentServices.GetAllStudents();
            return Ok(students);
        }

        [HttpGet]
        [Route("GetStudentById/{id}")]
        public async Task<ActionResult<StudentDto>> GetStudentById(int id)
        {
            var student = await _studentServices.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        [Route("AddStudent")]
        public async Task<IActionResult> AddStudent (StudentDto studentDto)
        {
            if (studentDto == null)
            {
                return BadRequest("Student is Empty");
            }
            await _studentServices.AddStudent(studentDto);
            return Ok();
        }


        [HttpPut]
        [Route("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(int id,StudentDto studentDto)
        {
            await _studentServices.UpdateStudent(id,studentDto);
            return Ok();
        }

        [HttpDelete("DeleteStudent/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var flag = false;
            if (id < 1 || id == null)
                return BadRequest();
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound();
            if (student.ParentId != null)
            {
                var students = await _context.Students.ToListAsync();
                foreach (var s in students)
                {
                    if (s.ParentId == student.ParentId && s.Id != id)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                {
                    var parent = await _context.Parents.FindAsync(student.ParentId);
                    _context.Parents.Remove(parent);

                }
            }
            await _studentServices.DeleteStudent(id);
            return Ok();
        }


    }
}
