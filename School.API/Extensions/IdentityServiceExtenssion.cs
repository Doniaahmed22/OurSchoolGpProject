using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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
            var builder = services.AddIdentityCore<AppUser>();

            builder = new IdentityBuilder(builder.UserType, builder.Services);

            builder.AddEntityFrameworkStores<SchoolIdentityDbContext>();
            builder.AddSignInManager<SignInManager<AppUser>>();


            //builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            //    .AddEntityFrameworkStores<SchoolIdentityDbContext>()
            //    .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options=>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"])),
                        ValidateIssuer = true,
                        ValidIssuer = _configuration["Token:Issuer"],
                        ValidateAudience = false
                    };
                });

            return services;
        }
    }
}
