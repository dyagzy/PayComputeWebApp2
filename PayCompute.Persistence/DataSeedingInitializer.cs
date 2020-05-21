using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayCompute.Persistence
{
    public static class DataSeedingInitializer
    {
        public static async Task UserAndRoleSeedAsync(UserManager<IdentityUser> userManager, 
                                                RoleManager<IdentityRole>  roleManager)
        {
            string[] roles = { "Admin", "Staff", "Manager" };
            foreach (var role in roles)
            {
               var roleExist = await  roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    IdentityResult result = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }


            //Create Admin User
            if (userManager.FindByEmailAsync("daniel.oyagha@gmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "daniel.oyagha@gmail.com",
                    Email = "daniel.oyagha@gmail.com"
                    
                };
                IdentityResult identityResult = userManager.CreateAsync(user, "Password1").Result;

                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }

            }

            //Create Manager User
            if (userManager.FindByEmailAsync("Manager@gmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "Manager@gmail.com",
                    Email = "Manager@gmail.com"

                };
                IdentityResult identityResult = userManager.CreateAsync(user, "Password1").Result;

                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Manager").Wait();
                }

            }
            //Create Staff User
            if (userManager.FindByEmailAsync("Jane.Doe@gmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "Jane.Doe@gmail.com",
                    Email = "Jane.Doe@gmail.com"

                };
                IdentityResult identityResult = userManager.CreateAsync(user, "Password1").Result;

                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Staff").Wait();
                }

            }

            //Create NoRole  User
            if (userManager.FindByEmailAsync("John.Doe@gmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "John.Doe@gmail.com",
                    Email = "John.Doe@gmail.com"

                };
                IdentityResult identityResult = userManager.CreateAsync(user, "Password1").Result;

               // No Role assigned to Mr. John Doe

            }



        }

    }
}