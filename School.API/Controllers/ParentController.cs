﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using School.Data.Context;
using School.Data.Entities;
using School.Data.Entities.Identity;
using School.Services.Dtos.ParentDto;
using School.Services.Dtos.SharedDto;
using School.Services.Services.ParentServices;
using School.Services.Tokens;
using School.Services.UserService;
using School.Services.UserService.Dtos;
using System.Text;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly IParentServices _parentServices;
        private readonly SchoolDbContext _context;
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;


        public ParentController(IParentServices parentServices , SchoolDbContext context , IUserService userService, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _parentServices = parentServices;
            _context = context;
            _userService = userService;
            _userManager = userManager;
            _configuration = configuration;

        }


        
        [HttpGet]
        [Route("GetParents")]
        //[Authorize(Roles ="Parent")]
        public async Task<ActionResult<IEnumerable<ParentDtoWithId>>> GetParents()
        {
            
            var Parents = await _parentServices.GetAllParents();
            return Ok(Parents);
        }


        [HttpGet]
        [Route("GetAllParents")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ParentDtoWithId>>> GetAllParents()
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

            if(parentDto.Name.Split(" ").Length < 2)
            {
                return BadRequest("name must be at least first and second");
            }

            if (parentDto.PhoneNumber.Length < 11 )
            {
                return BadRequest("phone number must be 11 number");
            }

            var validPrefixes = new[] { "011", "012", "015", "010" };
            if (!validPrefixes.Any(prefix => parentDto.PhoneNumber.StartsWith(prefix)))
            {
                return BadRequest("Your phone number must begin with 011, 010, 012, or 015.");
            }

            if (parentDto.GmailAddress.Split("@")[1] != "gmail.com" )
            {
                return BadRequest("this is invalid Gmail address it must terminate with @gmail.com");
            }


            RegisterDto registerDto = new RegisterDto
            {
                DisplayName = parentDto.Name,
                GmailAddress = parentDto.GmailAddress,
                Email = parentDto.Name.Split(" ")[0] + parentDto.Name.Split(" ")[1] + parentDto.PhoneNumber+"@school.com",
                Password = parentDto.Name.Split(" ")[0].ToUpper()+ parentDto.Name.Split(" ")[1].ToLower() + parentDto.PhoneNumber+ "!",
            };

            await _userService.Register(registerDto, "Parent");

            parentDto.Email = registerDto.Email;

            var user = await _userManager.FindByEmailAsync(registerDto.Email);
            parentDto.UserId = user.Id;

            await _parentServices.AddParent(parentDto);

            await _userService.SendEmail(registerDto);

            return Ok(parentDto);
        }


        [HttpPut]
        [Route("UpdateParent")]
        public async Task<IActionResult> UpdateParent(int id, ParentDto parentDto)
        {
            if (parentDto == null)
            {
                return BadRequest("Parent is Empty");
            }

            if (parentDto.Name.Split(" ").Length < 2)
            {
                return BadRequest("name must be at least your name and your father's name");
            }

            if (parentDto.PhoneNumber.Length < 11)
            {
                return BadRequest("phone number must be 11 number");
            }

            var validPrefixes = new[] { "011", "012", "015", "010" };
            if (!validPrefixes.Any(prefix => parentDto.PhoneNumber.StartsWith(prefix)))
            {
                return BadRequest("Your phone number must begin with 011, 010, 012, or 015.");
            }

            if (parentDto.GmailAddress.Split("@")[1] != "gmail.com")
            {
                return BadRequest("this is invalid Gmail address it must terminate with @gmail.com");
            }

            await _parentServices.UpdateParent(id, parentDto);
            return Ok();
        }

        [HttpDelete("DeleteParent/{id}")]
        public async Task<IActionResult> DeleteParent(int id)
        {

            var parent = await _context.Parents.FindAsync(id);

            var user = await _userManager.FindByIdAsync(parent.UserId);
/*
            if (user is null)
            {
                throw new Exception("User Email not found");
            }
            _userService.DeleteMessgeOfuserSender_Rec(user.Id);


           await _userManager.DeleteAsync(user);*/

            await _parentServices.DeleteParent(id);
            return Ok();
        }


        [HttpGet]
        [Route("GetStudentsOfParents")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<NameIdDto>>> GetStudentsOfParents(int id)
        {
            var Students = await _parentServices.GetStudentsOfParents(id);
            return Ok(Students);
        }


    }
}

