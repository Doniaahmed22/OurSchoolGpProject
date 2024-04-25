using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Data.Context;
using School.Data.Entities;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public StudentController(SchoolDbContext Context)
        {
            _context = Context;
        }

        [HttpGet]
        [Route("GetStudents")]
        public async Task<IActionResult> GetStudents()
        {
            return Ok(await _context.Students.ToListAsync());
        }

        [HttpGet]
        [Route("GetStudentById/{id}")]
        public async Task<IActionResult> GetStudentById(int? id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            var Student = await _context.Students.FindAsync(id);
            if (Student == null)
            {
                return NotFound();
            }
            return Ok(Student);
        }

        [HttpPost]
        [Route("AddStudent")]
        public async Task<IActionResult> AddStudent(Student student)
        {
            _context.Add(student);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Route("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(Student student)
        {
            if (student == null || student.Id == 0)
                return BadRequest();

            var NewStudent = await _context.Students.FindAsync(student.Id);
            if (NewStudent == null)
                return NotFound();
            NewStudent.Name = student.Name;
            NewStudent.Age = student.Age;
            NewStudent.Address = student.Address;
            NewStudent.PhoneNumber = student.PhoneNumber;
            NewStudent.BirthDay = student.BirthDay;
            NewStudent.Gender = student.Gender;
            NewStudent.Religion = student.Religion;
            NewStudent.Nationality = student.Nationality;
            NewStudent.ParentId = student.ParentId;
            NewStudent.ClassId = student.ClassId;
            NewStudent.DepartmentId = student.DepartmentId;
            NewStudent.LevelId = student.LevelId;

            await _context.SaveChangesAsync();
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
            if(student.ParentId != null)
            {
                var students = await _context.Students.ToListAsync();
                foreach (var s in students)
                {
                    if(s.ParentId == student.ParentId && s.Id != id)
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
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
