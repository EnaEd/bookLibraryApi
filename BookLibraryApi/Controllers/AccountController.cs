using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookLibraryApi.BusinesLayer.Intefaces;
using BookLibraryApi.BusinesLayer.ViewModels;
using BookLibraryApi.DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BookLibraryApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        public AccountController(IAccountService accountService, IConfiguration configuration, IEmailService emailService)
        {
            _accountService = accountService;
            _configuration = configuration;
            _emailService = emailService;
            
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
        public async Task<string> Registration(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }

            List<IdentityError> errors = new List<IdentityError>();
            if (!await _accountService.OnReigstration(model, errors))
            {
                return JsonConvert.SerializeObject(errors.Select(error => error.Description));
            }

            //generate mail confirmation
            string code = await _accountService.GeenerateConfirmationTokenAsync(model);
            string callbackUrl = Url.Action(
                "ConfirmEmail",
                "Account",
                new { userId = await _accountService.GetUserIdAsync(model), code = code },
                protocol: HttpContext.Request.Scheme);

            await _emailService.SendEmailAsync(
                model.Login,
                "Confirm your account",
                $"Confirm regisration by link:<a href='{callbackUrl}'>link</a>");

            return (Response.StatusCode = 200).ToString();
        }

        [HttpGet("ConfirmEmail")]
        public async Task<string> ConfirmEmail(string userId, string code)
        {
            return userId is null ||
                   code is null ||
                   !await _accountService.ConfirmEmailAsync(userId, code) ?
                   (Response.StatusCode = 400).ToString() :
                   (Response.StatusCode = 200).ToString();
        }
    }
}