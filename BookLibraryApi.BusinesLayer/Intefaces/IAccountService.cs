using BookLibraryApi.BusinesLayer.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookLibraryApi.BusinesLayer.Intefaces
{
    public interface IAccountService
    {
        Task<bool> OnLogin(UserViewModel user);
        Task<bool> OnReigstration(UserViewModel userl, List<IdentityError> errors);
        Task OnLogout();
    }
}
