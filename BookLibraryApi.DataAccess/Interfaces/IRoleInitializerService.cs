using BookLibraryApi.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BookLibraryApi.DataAccess.Interfaces
{
    public interface IRoleInitializerService
    {
        Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration);
    }
}
