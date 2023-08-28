using MealPlan.Business.Users.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace MealPlan.API.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;

        public AuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public LoginModel GenerateJwt(LoginModel user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
            };

            var token = GetToken(claims);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            user.Token = tokenString;
            return user;
        }

        private JwtSecurityToken GetToken(Claim[] authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
