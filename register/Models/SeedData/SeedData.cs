using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using register.Models.DBcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace register.Models.SeedData
{
    public class SeedData
    {
        ////Seed roles to Database
        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = { "Admin", "User" };

            foreach (var item in roles)
            {
                //create the roles and seed them to the database
                if (!await RoleManager.RoleExistsAsync(item))
                {
                    IdentityResult roleResult = await RoleManager.CreateAsync(new IdentityRole(item));
                }
            }
            
        }

        ////seed Admin to database
        public static async void Initialize(IServiceProvider service)
        {
            var context = service.GetRequiredService<AppDBcontext>();
            var userManager = service.GetRequiredService<UserManager<AppUser>>();

            if (!context.Users.Any())
            {
                AppUser user = new AppUser()
                {
                    Email="msaboor43@gmail.com",
                    UserName="Admin",

                };
                await userManager.CreateAsync(user, "1234qwer");
                var user1 = await userManager.FindByEmailAsync(user.Email);
                await userManager.AddToRoleAsync(user1, "Admin");
            }


        }
       



    }
}


