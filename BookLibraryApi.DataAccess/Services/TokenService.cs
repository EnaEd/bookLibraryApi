using BookLibraryApi.DataAccess.Entities;
using BookLibraryApi.DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryApi.DataAccess.Services
{
    public class TokenService : ITokenServie
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        public TokenService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<string> GenerateToken(string userName, string password)
        {
            ClaimsIdentity identity = await GetIdentity(userName, password);
            if (identity is null)
            {
                return string.Empty;
            }

            var jwt = new JwtSecurityToken(
                    issuer: _configuration["AuthOption:Issuer"],
                    audience: _configuration["AuthOption:Audience"],
                    notBefore: DateTime.Now,
                    claims: identity.Claims,
                    expires: DateTime.Now.Add(TimeSpan.FromMinutes(double.Parse(_configuration["AuthOption:LifeTime"]))),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["AuthOption:Key"])), SecurityAlgorithms.HmacSha256));
            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
            string token = JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return token;
        }

        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            User user = await _userManager.FindByEmailAsync(username);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)

                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims: claims, authenticationType: "Token", nameType: ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }
    }
}
