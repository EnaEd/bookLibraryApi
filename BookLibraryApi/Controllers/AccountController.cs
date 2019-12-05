using System.Collections.Generic;
using System.Threading.Tasks;
using BookLibraryApi.BusinesLayer.Intefaces;
using BookLibraryApi.BusinesLayer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Login")]
        public UserViewModel Login([FromBody] UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            return _accountService.OnLogin(user).GetAwaiter().GetResult() ? user : null;
        }

        [HttpPost("Logout")]
        [ValidateAntiForgeryToken]
        public async Task Logout()
        {
            await _accountService.OnLogout();
        }

        [HttpPost("Registration")]
        public async Task<UserViewModel> Registration(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            List<IdentityError> errors = new List<IdentityError>();
            return await _accountService.OnReigstration(user, errors) ? user : null;
        }
    }
}