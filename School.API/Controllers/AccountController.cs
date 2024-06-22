using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using School.Data.Entities.Identity;
using School.Services.EmailServices;
using School.Services.UserService;
using School.Services.UserService.Dtos;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace School.API.Controllers
{
    
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailService _emailService;

        public AccountController(IUserService userService, UserManager<AppUser> userManager, EmailService emailService)
        {
            _userService = userService;
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpPost]
        [Route("api/Login")]
        public async Task<ActionResult<UserDto>> Login (LoginDto input)
        {
             var user = await _userService.Login(input);

            if(user is null)
            {
                return Unauthorized("user not Authorize");
            }

            return Ok(user);
        }



        [HttpPost]
        [Route("api/Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto input, string role)
        {
            var user = await _userService.Register(input, role);

            if (user is null)
            {
                return BadRequest("Email already Exists");
            }

            return Ok(user);
        }


        [HttpGet]
        [Route("api/GetCurrentUserDetails")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUserDetails ()
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName
            };
        }


        /*
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("User not found");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            //var resetUrl = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);
            var resetUrl = $"{Request.Scheme}://{Request.Host}/api/account/reset-password?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(model.GmailAddress)}";


            await _emailService.SendEmailAsync(model.GmailAddress , "Reset Password", $"<a href='{resetUrl}'>Click here to reset your password</a>");

            return Ok("Password reset link has been sent to your email");
        }
        */



        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (succeeded, errors) = await _userService.ChangePasswordAsync(model);

            if (succeeded)
            {
                return Ok("Password changed successfully.");
            }
            else
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError("", error);
                }
                return BadRequest(ModelState);
            }
        }

    }
}
