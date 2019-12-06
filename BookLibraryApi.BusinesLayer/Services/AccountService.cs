using BookLibraryApi.BusinesLayer.Intefaces;
using BookLibraryApi.BusinesLayer.ViewModels;
using BookLibraryApi.DataAccess.Entities;
using BookLibraryApi.DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibraryApi.BusinesLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenServie _tokenServie;
        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, 
            IConfiguration configuration,ITokenServie tokenServie)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _tokenServie = tokenServie;
        }

        public async Task<string> OnLogin(UserViewModel user)
        {
            SignInResult result =
                await _signInManager.PasswordSignInAsync(user.Login, user.Password, user.RememberMe, false);
            return result.Succeeded ? await _tokenServie.GenerateToken(user.Login, user.Password) :
                _configuration["ErrorsMessage:Unauthorize:errorCode"];
                

        }

        public async Task OnLogout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> OnReigstration(UserViewModel userModel, List<IdentityError> errors)
        {
            User user = new User { Email = userModel.Login, UserName = userModel.Login };
            IdentityResult result = await _userManager.CreateAsync(user, userModel.Password);
            if (!result.Succeeded)
            {
                result.Errors.ToList().ForEach((error) => errors.Add(error));
                return false;
            }
            await _userManager.AddToRoleAsync(user, _configuration["Roles:Reader"]);
            //TODO wait email confimation
            await _signInManager.SignInAsync(user, false);
            return true;
        }
    }
}
