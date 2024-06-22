using Microsoft.AspNetCore.Identity;
using School.Data.Entities.Identity;
using School.Services.EmailServices;
using School.Services.Tokens;
using School.Services.UserService.Dtos;

namespace School.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly EmailService _emailService;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, EmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
        }
        public async Task<UserDto> Login(LoginDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);

            if(user is null)
            {
                return null;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, input.Password, false);
            
            if(!result.Succeeded)
            {
                throw new Exception("login failed");
      
            }
            return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.GenerateToken(user)
            };

        }

        public async Task<UserDto> Register(RegisterDto input , string role)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);

            if (user is not null)
            {
                return null;
            }

            var appUser = new AppUser
            {
                DisplayName = input.DisplayName,
                Email = input.Email,
                UserName = input.Email.Split("@")[0]
            };

            var result = await _userManager.CreateAsync(appUser,input.Password);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.Select(x => x.Description).FirstOrDefault());
            }

            await _userManager.AddToRoleAsync(appUser , role);

            var receiver = input.GmailAddress;
            // Prepare email
            string subject = "Welcome to Our Service";
            string body = $"Hello {input.DisplayName},<br/>Your account has been created.<br/> Email : {input.Email}, Password: {input.Password}";

            // Send email
            await _emailService.SendEmailAsync(receiver, subject, body);
            


            return new UserDto
            {
                Email = input.Email,
                DisplayName = input.DisplayName,
                Token = _tokenService.GenerateToken(appUser)
            };

        }


        public async Task<(bool Succeeded, string[] Errors)> ChangePasswordAsync(ChangePasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return (false, new[] { "User not found." });
            }


            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return (true, new string[] { });
            }
            else
            {
                return (false, result.Errors.Select(e => e.Description).ToArray());
            }
        }


    }
}
