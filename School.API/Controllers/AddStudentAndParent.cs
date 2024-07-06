using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using School.Data.Context;
using School.Data.Entities.Identity;
using School.Services.Dtos.ParentDto;
using School.Services.Dtos.ParentStudentDto;
using School.Services.Dtos.StudentDto;
using School.Services.Services.ParentServices;
using School.Services.Services.ParentStudent;
using School.Services.UserService;
using School.Services.UserService.Dtos;

namespace School.API.Controllers
{
    [ApiController]
    public class AddStudentAndParent : ControllerBase
    {
        private readonly IAddParentStudent _parentStudentServices;
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;


        public AddStudentAndParent(IAddParentStudent parentStudentServices, IUserService userService, UserManager<AppUser> userManager)
        {
            _parentStudentServices = parentStudentServices;
            _userService = userService;
            _userManager = userManager;

        }


        [HttpPost]
        [Route("AddParentAndStudent")]
        public async Task<IActionResult> AddParentAndStudent(ParentStudentDto parentStudentDto)
        {
            if (parentStudentDto == null)
            {
                return BadRequest("Parent and student is Empty");
            }

            if (parentStudentDto.ParentName.Split(" ").Length < 2)
            {
                return BadRequest("Parent name must be at least first and second");
            }

            if (parentStudentDto.ParentPhoneNumber.Length != 11)
            {
                return BadRequest("Parent phone number must be 11 number");
            }

            var validPrefixes = new[] { "011", "012", "015", "010" };
            if (!validPrefixes.Any(prefix => parentStudentDto.ParentPhoneNumber.StartsWith(prefix)))
            {
                return BadRequest("Parent phone number must begin with 011, 010, 012, or 015.");
            }

            if (parentStudentDto.ParentGmailAddress.Split("@")[1] != "gmail.com")
            {
                return BadRequest("Parent Gmail is invalid it must terminate with @gmail.com");
            }


            RegisterDto parentRegisterDto = new RegisterDto
            {
                DisplayName = parentStudentDto.ParentName,
                GmailAddress = parentStudentDto.ParentGmailAddress,
                Email = parentStudentDto.ParentName.Split(" ")[0] + parentStudentDto.ParentName.Split(" ")[1] + parentStudentDto.ParentPhoneNumber + "@school.com",
                Password = parentStudentDto.ParentName.Split(" ")[0].ToUpper() + parentStudentDto.ParentName.Split(" ")[1].ToLower() + parentStudentDto.ParentPhoneNumber + "!",
            };

            await _userService.Register(parentRegisterDto, "Parent");

            parentStudentDto.ParentEmail = parentRegisterDto.Email;

            var parUser = await _userManager.FindByEmailAsync(parentRegisterDto.Email);
            parentStudentDto.ParentUserId = parUser.Id;




            if (parentStudentDto.StudentName.Split(" ").Length < 2)
            {
                return BadRequest("name must be at least your name and your father's name");
            }

            if (parentStudentDto.StudentPhoneNumber.Length < 11)
            {
                return BadRequest("phone number must be 11 number");
            }

            if (!validPrefixes.Any(prefix => parentStudentDto.StudentPhoneNumber.StartsWith(prefix)))
            {
                return BadRequest("Your phone number must begin with 011, 010, 012, or 015.");
            }

            if (parentStudentDto.StudentGmailAddress.Split("@")[1] != "gmail.com")
            {
                return BadRequest("this is invalid Gmail address it must terminate with @gmail.com");
            }

            if (parentStudentDto.StudebtAge < 14)
            {
                return BadRequest("Age must be greater than 14 years");
            }

            if (parentStudentDto.StudentGender != 'm' && parentStudentDto.StudentGender != 'M' && parentStudentDto.StudentGender != 'f' && parentStudentDto.StudentGender != 'F')
            {
                return BadRequest("Gender must be f or F for Femail OR m or M for Mail");
            }

            RegisterDto StudentRegisterDto = new RegisterDto
            {
                DisplayName = parentStudentDto.StudentName,
                GmailAddress = parentStudentDto.StudentGmailAddress,
                Email = parentStudentDto.StudentName.Split(" ")[0] + parentStudentDto.StudentName.Split(" ")[1] + parentStudentDto.StudentPhoneNumber + "@school.com",
                Password = parentStudentDto.StudentName.Split(" ")[0].ToUpper() + parentStudentDto.StudentName.Split(" ")[1].ToLower() + parentStudentDto.StudentBirthDay.Day + parentStudentDto.StudentBirthDay.Month + parentStudentDto.StudentBirthDay.Year + "!",
            };

            await _userService.Register(StudentRegisterDto, "Student");
            parentStudentDto.StudentEmail = StudentRegisterDto.Email;

            var stdUser = await _userManager.FindByEmailAsync(StudentRegisterDto.Email);
            parentStudentDto.StudentUserId = stdUser.Id;



            await _parentStudentServices.AddParentAndStudent(parentStudentDto);




            await _userService.SendEmail(StudentRegisterDto);
            await _userService.SendEmail(parentRegisterDto);






            return Ok(parentStudentDto);
        }

    }
}
