using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using School.Data.Entities.Identity;
using School.Services.EmailServices;
using School.Services.Tokens;
using School.Services.UserService;
using School.Services.UserService.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace School.API.Controllers
{

    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailService _emailService;
        private readonly TokenBlacklistService _tokenBlacklistService;
        private readonly IConfiguration _configuration;
        private readonly TokenValidationParameters _tokenValidationParameters;


        public AccountController(IUserService userService, UserManager<AppUser> userManager, EmailService emailService, TokenBlacklistService tokenBlacklistService, IConfiguration configuration)
        {
            _userService = userService;
            _userManager = userManager;
            _emailService = emailService;
            _tokenBlacklistService = tokenBlacklistService;
            _configuration = configuration;
            _tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Token:Issuer"],
                ValidAudience = _configuration["Token:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"])),
            };


        }

        [HttpPost]
        [Route("api/Login")]
        public async Task<ActionResult<GetLoginDetails>> Login (LoginDto input)
        {
             var user = await _userService.Login(input);

            if(user == null)
            {
                return BadRequest("E-Mail or password not correct");
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
//        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<UserDto>> GetCurrentUserDetails ()
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,

        };
        }




        [HttpPost]
        [Route("api/forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("User not found");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = Url.Action("ResetPassword", "Account",
                                       new { token, email = model.Email }, Request.Scheme);

            // Send the reset link to the secondary email
            await _emailService.SendEmailAsync(model.GmailAddress, "Reset Password",
                                              $"Please reset your password by clicking here: {resetLink}");

            return Ok();
        }




        [HttpPost]
        [Route("api/reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("User not found");

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }









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



        [HttpPost("signout")]
        public IActionResult SignOut()
        {

            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return BadRequest("Authorization header is missing.");
            }

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is missing.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                // Validate the token
                tokenHandler.ValidateToken(token, _tokenValidationParameters, out SecurityToken validatedToken);

                // If valid, blacklist the token
                _tokenBlacklistService.BlacklistToken(token);

                return Ok("Signed out successfully.");
            }
            catch (SecurityTokenException)
            {
                return Unauthorized("Invalid token.");
            }


        }
    }
}
