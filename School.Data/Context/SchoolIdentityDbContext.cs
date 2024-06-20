using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using School.Data.Entities.Identity;

namespace School.Data.Context
{
    public class SchoolIdentityDbContext : IdentityDbContext<AppUser>
    {
        public SchoolIdentityDbContext (DbContextOptions<SchoolIdentityDbContext> options) : base(options) 
        { 
        }

    }
}
