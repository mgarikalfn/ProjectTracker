using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ProjectTracker.Domain.Identity;

namespace ProjectTracker.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(RoleManager<AppRole> roleManager)
        {
            string[] roles = { "Executive", "Manager", "Developer" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new AppRole { Name = role });
                }
            }
        }


    }
}