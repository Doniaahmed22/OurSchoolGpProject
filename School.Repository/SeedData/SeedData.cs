using Microsoft.AspNetCore.Identity;
using School.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.SeedData
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "Student","Parent","Teacher" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var adminUser = new AppUser
            {
                UserName = "OurSchool",
                Email = "OurSchool@gmail.com",
                DisplayName = "Our School"
            };

            string userPassword = "Password@123";
            var user = await userManager.FindByEmailAsync(adminUser.Email);

            if (user == null)
            {
                var createPowerUser = await userManager.CreateAsync(adminUser, userPassword);
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
