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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenServie _tokenServie;
        private readonly IEmailService _emailService;
        private readonly IRoleInitializerService _roleInitializer;
        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager,
            IConfiguration configuration, ITokenServie tokenServie, IEmailService emailService,
            IRoleInitializerService roleInitializer,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _tokenServie = tokenServie;
            _emailService = emailService;
            _roleInitializer = roleInitializer;
            _roleManager = roleManager;
            
        }

        public async Task<bool> ConfirmEmailAsync(string userId, string code)
        {

            return await _userManager.FindByIdAsync(userId) is User user ?
                   _userManager.ConfirmEmailAsync(user, code).GetAwaiter().GetResult().Succeeded:
                   false;

        }

        public async Task<string> GeenerateConfirmationTokenAsync(UserViewModel user)
        {
            User result = await _userManager.FindByEmailAsync(user.Login);
            if (result is null)
            {
                return null;
            }
            return await _userManager.GenerateEmailConfirmationTokenAsync(result);
        }

        public async Task<string> GetUserIdAsync(UserViewModel model)
        {
            return await _userManager.FindByEmailAsync(model.Login) is User user ? user.Id : null;
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
            await _roleInitializer.InitializeAsync(_userManager, _roleManager, _configuration);

            User user = new User { Email = userModel.Login, UserName = userModel.Login };
            IdentityResult result = await _userManager.CreateAsync(user, userModel.Password);
            if (!result.Succeeded)
            {
                result.Errors.ToList().ForEach((error) => errors.Add(error));
                return false;
            }
            await _userManager.AddToRoleAsync(user, _configuration["Roles:Reader"]);
            return true;
        }
    }
}
