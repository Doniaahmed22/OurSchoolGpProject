using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Data.Context;
using School.Data.Entities;
using School.Data.Entities.Identity;
using School.Services.Dtos.ParentDto;
using School.Services.Dtos.StudentDto;
using School.Services.Services.ParentServices;
using School.Services.Services.StudentServices;
using School.Services.UserService;
using School.Services.UserService.Dtos;

namespace School.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly IParentServices _parentServices;
        private readonly SchoolDbContext _context;
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public ParentController(IParentServices parentServices , SchoolDbContext context , IUserService userService, UserManager<AppUser> userManager)
        {
            _parentServices = parentServices;
            _context = context;
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("GetParents")]
        public async Task<ActionResult<IEnumerable<ParentDtoWithId>>> GetParents()
        {
            var Parents = await _parentServices.GetAllParents();
            return Ok(Parents);
        }

        [HttpGet]
        [Route("GetParentById/{id}")]
        public async Task<ActionResult<ParentDtoWithId>> GetParentById(int id)
        {
            var parent = await _parentServices.GetParentById(id);
            if (parent == null)
            {
                return NotFound();
            }
            return Ok(parent);
        }

        [HttpPost]
        [Route("AddParent")]
        public async Task<IActionResult> AddParent(ParentDto parentDto)
        {
            if (parentDto == null)
            {
                return BadRequest("Parent is Empty");
            }

            RegisterDto registerDto = new RegisterDto
            {
                DisplayName = parentDto.Name,
                GmailAddress = parentDto.GmailAddress,
                Email = parentDto.Name.Split(" ")[0] + parentDto.PhoneNumber+"@gmail.com",
                Password = parentDto.Name.Split(" ")[0].ToUpper()+ parentDto.Name.Split(" ")[1].ToLower() + parentDto.PhoneNumber+ "!",
            };

            _userService.Register(registerDto, "Parent");

            parentDto.Email = registerDto.Email;

            await _parentServices.AddParent(parentDto);
            return Ok(parentDto);
        }


        [HttpPut]
        [Route("UpdateParent")]
        public async Task<IActionResult> UpdateParent(int id, ParentDto parentDto)
        {
            await _parentServices.UpdateParent(id, parentDto);
            return Ok();
        }

        [HttpDelete("DeleteParent/{id}")]
        public async Task<IActionResult> DeleteParent(int id)
        {

            var parent = await _context.Parents.FindAsync(id);

            var user = await _userManager.FindByEmailAsync(parent.Email);

            if (user is null)
            {
                throw new Exception("User Email not found");
            }

            _userManager.DeleteAsync(user);

            await _parentServices.DeleteParent(id);
            return Ok();
        }

    }
}


