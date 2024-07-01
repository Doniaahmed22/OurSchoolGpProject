using School.Services.UserService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.UserService
{
    public interface IUserService
    {
        Task<GetRegisteerDto> Register(RegisterDto input, string role);
        Task<string> SendEmail(RegisterDto input);

        Task<string> Login(LoginDto loginDto);

        Task<(bool Succeeded, string[] Errors)> ChangePasswordAsync(ChangePasswordModel model);
    }
}
