using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using School.API.Extensions;
using School.Data.Context;
using School.Data.Entities.Identity;
using School.Repository.SeedData;
using School.Services.Dtos.EmailSending;
using School.Services.EmailServices;
using School.Services.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace School.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<SchoolDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<SchoolIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });


            builder.Services.AddControllersWithViews();

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<SchoolIdentityDbContext>()
                .AddDefaultTokenProviders();

            builder.Configuration.AddEnvironmentVariables();

            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
            builder.Services.AddSingleton<EmailService>();
            builder.Services.AddLogging();
            //builder.Services.AddSingleton<EmailService>();

            builder.Services.AddSchoolServices();
            builder.Services.AddIdentityServices(builder.Configuration);


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerDocumentation();
            builder.Services.AddSwaggerGen();


            builder.Services.AddCors(corsoption =>
            {
                corsoption.AddPolicy("MyPolicy", policybuilder =>
                {
                    policybuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddSingleton<TokenBlacklistService>();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("RequireParentRole", policy => policy.RequireRole("Parent"));
                options.AddPolicy("RequireStudentRole", policy => policy.RequireRole("Student"));
                options.AddPolicy("RequireTeacherRole", policy => policy.RequireRole("Teacher"));
            })
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Token:Issuer"],
                        ValidAudience = builder.Configuration["Token:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"])),
                        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                        {
                            var tokenBlacklistService = builder.Services.BuildServiceProvider().GetRequiredService<TokenBlacklistService>();
                            var jwtToken = securityToken as JwtSecurityToken;
                            if (jwtToken != null && tokenBlacklistService.IsTokenBlacklisted(jwtToken.RawData))
                            {
                                return false;
                            }
                            return expires != null && expires > DateTime.UtcNow;
                        }
                    };


                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            var tokenBlacklistService = context.HttpContext.RequestServices.GetRequiredService<TokenBlacklistService>();
                            var token = context.SecurityToken as JwtSecurityToken;
                            if (token != null && tokenBlacklistService.IsTokenBlacklisted(token.RawData))
                            {
                                context.Fail("This token is blacklisted.");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            //builder.Services.AddScoped<UserRoleFilter>(); // Register the custom action filter


            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors("MyPolicy");

            /*
            //builder.Services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            //    options.AddPolicy("RequireParentRole", policy => policy.RequireRole("Parent"));
            //    options.AddPolicy("RequireStudentRole", policy => policy.RequireRole("Student"));
            //    options.AddPolicy("RequireTeacherRole", policy => policy.RequireRole("Teacher"));
            //});
            */


            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            // Seed data
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await SeedData.InitializeAsync(services, userManager, roleManager);
            }

            app.Run();
        }
    }
}
