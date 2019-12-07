using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public async Task<IActionResult> Login([FromBody] UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(_configuration["ErrorsMessage:NoValidData:message"]);
            }

            var result = await _accountService.OnLogin(user);
           
            if (!_configuration.AsEnumerable().ToList().
                Where(section => section.Key.Contains("innerCode")).
                Any(item => item.Value.Contains(result)))
            {
                return Ok(result);
            }
            var errorSection = _configuration.AsEnumerable().ToList().
                Where(section => section.Key.Contains("innerCode")).
                First(item => item.Value.Contains(result)).Key.Replace(":innerCode", "");

            
            if (Convert.ToInt32(_configuration[$"{errorSection}:errorCode"])==(int)HttpStatusCode.BadRequest)
            {
                return BadRequest(_configuration[$"{errorSection}:message"]);
            }
            if (Convert.ToInt32(_configuration[$"{errorSection}:errorCode"]) == (int)HttpStatusCode.Unauthorized)
            {
                return Unauthorized(_configuration[$"{errorSection}:message"]);
            }
            
            return BadRequest(_configuration["ErrorsMessage:UnhandleExption:message"]);

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