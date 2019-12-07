using BookLibraryApi.DataAccess.Entities;
using BookLibraryApi.DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BookLibraryApi.DataAccess.Services
{
    public class RoleInitializerService: IRoleInitializerService
    {
        public async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            
            if (await roleManager.FindByNameAsync(configuration["Roles:Admin"]) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(configuration["Roles:Admin"]));
            }
            if (await roleManager.FindByNameAsync(configuration["Roles:Reader"]) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(configuration["Roles:Reader"]));
            }
            if (await roleManager.FindByNameAsync(configuration["Roles:Librarian"]) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(configuration["Roles:Librarian"]));
            }
        }
    }
}
