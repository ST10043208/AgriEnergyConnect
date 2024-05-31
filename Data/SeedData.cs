using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Identity;

namespace AgriEnergyConnect.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Employee", "Farmer" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            ApplicationUser user = await userManager.FindByEmailAsync("admin@example.com");

            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                };
                await userManager.CreateAsync(user, "Admin@123");
            }
            await userManager.AddToRoleAsync(user, "Employee");
        }
    }
}
