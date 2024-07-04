using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using School.Data.Entities.Identity;
using School.Repository.Interfaces;
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
        private readonly IParentRepository _parentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, EmailService emailService,IParentRepository parentRepository, IStudentRepository studentRepository, ITeacherRepository teacherRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
            _parentRepository = parentRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
        }


        public async Task<GetLoginDetails> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return null;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count != 1)
            {
                return null;
            }

            var token = _tokenService.GenerateToken(user, roles.First());

            var id = 0; 
            if(roles.First() == "Student")
            {
                id = await _studentRepository.GetStudentByUserId(user.Id);
            }
            else if (roles.First() == "Parent")
            {
                id = await _parentRepository.GetParentByUserId(user.Id);
            }
            else if (roles.First() == "Teacher")
            {
                id = await _teacherRepository.GetTeacherByUserId(user.Id);
            }
            //else if (roles.First() == "Admin")
            //{
            //    id = 0;
            //}

            return new GetLoginDetails
            {
                userId = user.Id,
                role = roles.First(),
                id = id,
                token = token
            };

        }


        public async Task<GetRegisteerDto> Register(RegisterDto input , string role)
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

          
                        
            if( !(await _userManager.AddToRoleAsync(appUser , role)).Succeeded  )
            {
                throw new Exception(result.Errors.Select(x => x.Description).FirstOrDefault());
            }

            return new GetRegisteerDto
            {
                Email = input.Email,
                DisplayName = input.DisplayName
            };

        }

        public async Task<string> SendEmail (RegisterDto input)
        {
            var receiver = input.GmailAddress;
            // Prepare email
            string subject = "Welcome to Our Service";
            string body = $"Hello {input.DisplayName},<br/>Your account has been created.<br/> Email : {input.Email}, Password: {input.Password}";

            // Send email
            await _emailService.SendEmailAsync(receiver, subject, body);

            return "done";
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
