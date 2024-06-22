using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using School.API.Extensions;
using School.Data.Context;
using School.Data.Entities.Identity;
using School.Repository.SeedData;
using School.Services.Dtos.EmailSending;
using School.Services.EmailServices;

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


            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //.AddEntityFrameworkStores<AppUser>();

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
            builder.Services.AddSwaggerDocumentation();
            //builder.Services.AddSwaggerGen();


            builder.Services.AddCors(corsoption =>
            {
                corsoption.AddPolicy("MyPolicy", policybuilder =>
                {
                    policybuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole",policy => policy.RequireRole("Admin"));
                options.AddPolicy("RequireParentRole",policy => policy.RequireRole("Parent"));
                options.AddPolicy("RequireStudentRole",policy => policy.RequireRole("Student"));
                options.AddPolicy("RequireTeacherRole",policy => policy.RequireRole("Teacher"));
            });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");

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
