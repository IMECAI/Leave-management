using Leave_management.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Leave_management
{
    public static class SeedData
    {
        public static void Seed(UserManager<Employee> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            SeedRole(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedUsers(UserManager<Employee> userManager)
        {
            //Checks to see if their is a user "admin"
            if (userManager.FindByNameAsync("admin@localhost.com").Result == null)
            {
                //If object is null create admin
                var user = new Employee
                {
                    UserName = "admin@host.com",
                    Email = "admin@host.com"
                };
                var result = userManager.CreateAsync(user, "P@ssword1").Result;
                //Was the results successful or not
                if (result.Succeeded)
                {
                    //if user creation successful assign this user to role/ Administrator
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }
        
        private static void SeedRole(RoleManager<IdentityRole> roleManager)
        {
            //if the role "Administrator" does not exists then create it
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Administrator"
                };
                roleManager.CreateAsync(role);
            }
            //if the role "Employee" does not exists then create it
            if (!roleManager.RoleExistsAsync("Employee").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Employee"
                };
                roleManager.CreateAsync(role);
            }
        }
    }
}
