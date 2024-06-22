using School.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Tokens
{
    public interface ITokenService
    {
        string GenerateToken(AppUser appUser);
    }
}
