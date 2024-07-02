﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using School.Data.Entities.Identity;
using School.Services.EmailServices;
using School.Services.Tokens;
using School.Services.UserService.Dtos;
using System.Security.Claims;

namespace School.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly EmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, EmailService emailService, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;

        }


        public async Task<string> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return "1";
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return "2";
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count != 1)
            {
                return null;
            }

            var token = _tokenService.GenerateToken(user, roles.First());

            return token;
        }


        public async Task<GetRegisteerDto> Register(RegisterDto input, string role)
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

            var result = await _userManager.CreateAsync(appUser, input.Password);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.Select(x => x.Description).FirstOrDefault());
            }

            //var receiver = input.GmailAddress;
            //// Prepare email
            //string subject = "Welcome to Our Service";
            //string body = $"Hello {input.DisplayName},<br/>Your account has been created.<br/> Email : {input.Email}, Password: {input.Password}";

            //// Send email
            //await _emailService.SendEmailAsync(receiver, subject, body);           

            if (!(await _userManager.AddToRoleAsync(appUser, role)).Succeeded)
            {
                throw new Exception(result.Errors.Select(x => x.Description).FirstOrDefault());
            }

            return new GetRegisteerDto
            {
                Email = input.Email,
                DisplayName = input.DisplayName
            };

        }

        public async Task<string> SendEmail(RegisterDto input)
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
        public string GetAuthenticatedUserId()
        {
            var userId = "";
            if (_httpContextAccessor.HttpContext != null)
            {
                var NameIdentifier = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                userId = NameIdentifier;
            }
            return userId;
        }



    }
}
