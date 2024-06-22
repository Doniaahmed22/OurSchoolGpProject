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
        Task<UserDto> Register(RegisterDto input, string role);
        Task<UserDto> Login(LoginDto input);
        Task<(bool Succeeded, string[] Errors)> ChangePasswordAsync(ChangePasswordModel model);
    }
}
