using BookLibraryApi.BusinesLayer.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace BookLibraryApi.BusinesLayer.Intefaces
{
    public interface IAccountService
    {
        Task<string> OnLogin(UserViewModel user);
        Task<bool> OnReigstration(UserViewModel user, List<IdentityError> errors);
        Task<string> GeenerateConfirmationTokenAsync(UserViewModel user);
        Task<bool> ConfirmEmailAsync(string userId, string code);
        Task<string> GetUserIdAsync(UserViewModel user);
        Task OnLogout();
    }
}
