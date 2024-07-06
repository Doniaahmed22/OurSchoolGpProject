using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Data.Context;
using School.Data.Entities;
using School.Data.Entities.Identity;
using School.Services.Dtos.GradesDto;
using School.Services.Dtos.ParentDto;
using School.Services.Dtos.StudentDto;
using School.Services.Dtos.SubjectDto;
using School.Services.Services.StudentServices;
using School.Services.UserService;
using School.Services.UserService.Dtos;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentServices _studentServices;
        private readonly SchoolDbContext _context;
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public StudentController(IStudentServices studentServices, SchoolDbContext context, IUserService userService, UserManager<AppUser> userManager)
        {
            _studentServices = studentServices;
            _context = context;
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("GetStudents")]
        //[Authorize(Roles ="Student")]
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
        public async Task<IActionResult> AddStudent(StudentDto studentDto)
        {
            if (studentDto == null)
            {
                return BadRequest("Student is Empty");
            }

            if (studentDto.Name.Split(" ").Length < 2)
            {
                return BadRequest("name must be at least your name and your father's name");
            }

            if (studentDto.PhoneNumber.Length < 11)
            {
                return BadRequest("phone number must be 11 number");
            }

            var validPrefixes = new[] { "011", "012", "015", "010" };
            if (!validPrefixes.Any(prefix => studentDto.PhoneNumber.StartsWith(prefix)))
            {
                return BadRequest("Your phone number must begin with 011, 010, 012, or 015.");
            }

            if (studentDto.GmailAddress.Split("@")[1] != "gmail.com")
            {
                return BadRequest("this is invalid Gmail address it must terminate with @gmail.com");
            }

            if (studentDto.Age < 14)
            {
                return BadRequest("Age must be greater than 14 years");
            }

            if (studentDto.Gender != 'm' && studentDto.Gender != 'M' && studentDto.Gender != 'f' && studentDto.Gender != 'F')
            {
                return BadRequest("Gender must be f or F for Femail OR m or M for Mail");
            }




            RegisterDto registerDto = new RegisterDto
            {
                DisplayName = studentDto.Name,
                GmailAddress = studentDto.GmailAddress,
                Email = studentDto.Name.Split(" ")[0] + studentDto.Name.Split(" ")[1] + studentDto.PhoneNumber +"@school.com",
                Password = studentDto.Name.Split(" ")[0].ToUpper() + studentDto.Name.Split(" ")[1].ToLower() + studentDto.BirthDay.Day + studentDto.BirthDay.Month + studentDto.BirthDay.Year + "!",
            };

            await _userService.Register(registerDto,"Student");
            studentDto.Email = registerDto.Email;

            var user = await _userManager.FindByEmailAsync(registerDto.Email);
            studentDto.UserId = user.Id;
            
            await _studentServices.AddStudent(studentDto);
            await _userService.SendEmail(registerDto);
            return Ok(studentDto);
        }


        [HttpPut]
        [Route("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(int id, StudentDto studentDto)
        {
            if (studentDto == null)
            {
                return BadRequest("Student is Empty");
            }

            if (studentDto.Name.Split(" ").Length < 2)
            {
                return BadRequest("name must be at least your name and your father's name");
            }

            if (studentDto.PhoneNumber.Length < 11)
            {
                return BadRequest("phone number must be 11 number");
            }

            var validPrefixes = new[] { "011", "012", "015", "010" };
            if (!validPrefixes.Any(prefix => studentDto.PhoneNumber.StartsWith(prefix)))
            {
                return BadRequest("Your phone number must begin with 011, 010, 012, or 015.");
            }

            if (studentDto.GmailAddress.Split("@")[1] != "gmail.com")
            {
                return BadRequest("this is invalid Gmail address it must terminate with @gmail.com");
            }

            if (studentDto.Age < 14)
            {
                return BadRequest("Age must be greater than 14 years");
            }

            if (studentDto.Gender != 'm' && studentDto.Gender != 'M' && studentDto.Gender != 'f' && studentDto.Gender != 'F')
            {
                return BadRequest("Gender must be f or F for Femail OR m or M for Mail");
            }


            await _studentServices.UpdateStudent(id, studentDto);
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
                    var par = await _userManager.FindByEmailAsync(parent.Email);

                   await _userManager.DeleteAsync(par);
                    _context.Parents.Remove(parent);

                }
            }


            var user2 = await _userManager.FindByEmailAsync(student.Email);

            if (user2 is null)
            {
                throw new Exception("User Email not found");
            }
           await _userManager.DeleteAsync(user2);

            await _studentServices.DeleteStudent(id);
            return Ok();
        }

        [HttpGet("GetStudentsByClassId/{ClassId:int}")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudentsByClassId(int ClassId)
        {
            var students = await _studentServices.GetStudentsByClassId(ClassId);
            if (students == null)
                return NotFound();
            return Ok(students);
        }
        [HttpGet("GetStudentsWithAbsentDays")]
        public async Task<IActionResult> GetStudentsWithAbsentDays()
        {
            var students = await _studentServices.GetStudentsWithAbsentDays();
            if (students == null)
                return NotFound();
            return Ok(students);
        }
        [HttpGet("GetStudentsWithParentByClassID/{classid:int}")]
        public async Task<IActionResult> GetStudentsWithParentByClassID(int classid)
        {
            var students = await _studentServices.GetStudentsWithParentByClassID(classid);
            if (students == null)
                return NotFound();
            return Ok(students);
        }

        [HttpGet("SeacrhStudentsByClassIDStudentName/{classid:int}/{StudentName}")]
        public async Task<IActionResult> SeacrhStudentsByClassIDStudentName(int classid,string StudentName)
        {
            var students = await _studentServices.GetStudentsWithParentByClassID(classid, StudentName);
            if (students == null)
                return NotFound();
            return Ok(students);
        }

        [HttpGet("GetStudentsWithParent")]
        public async Task<IActionResult> GetStudentsWithParent()
        {
            var students = await _studentServices.GetStudentsWithParent();
            if (students == null)
                return NotFound();
            return Ok(students);
        }
    }
}
