using System.Collections.Generic;
using System.Threading.Tasks;
using BookLibraryApi.BusinesLayer.Intefaces;
using BookLibraryApi.BusinesLayer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BookLibraryApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;
        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<string> Login([FromBody] UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            //TODO architecture?
            var result = await _accountService.OnLogin(user);//getAwaiter().getResult()?
            return result.Equals(_configuration["ErrorsMessage:Unauthorize:errorCode"]) ?
                (Response.StatusCode = 401).ToString() :
                await _accountService.OnLogin(user);
        }

        [HttpGet("Logout")]
        //[ValidateAntiForgeryToken]
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