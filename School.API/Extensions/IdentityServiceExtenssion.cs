using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using School.Data.Context;
using School.Data.Entities.Identity;
using System.Text;

namespace School.API.Extensions
{
    public static class IdentityServiceExtenssion
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services , IConfiguration _configuration)
        {
            services.AddDbContext<SchoolDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentityCore<AppUser>()
                    .AddEntityFrameworkStores<SchoolDbContext>()
                    .AddSignInManager<SignInManager<AppUser>>();

            // Other configurations and services
            services.AddControllers();

            return services;
        }
    }
}
