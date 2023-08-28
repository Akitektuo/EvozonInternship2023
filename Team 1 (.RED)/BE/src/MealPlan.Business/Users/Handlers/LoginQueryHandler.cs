using MealPlan.Business.Exceptions;
using MealPlan.Business.Users.Models;
using MealPlan.Business.Users.Queries;
using MealPlan.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.Business.Users.Handlers
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, UserDetails>
    {
        private readonly MealPlanContext _context;

        private readonly IConfiguration _configuration;

        public LoginQueryHandler(MealPlanContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<UserDetails> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Users
                .Where(u => u.Email.Equals(request.Email) && u.Password.Equals(request.Password))
                .ToUserDetails()
                .SingleOrDefaultAsync();

            if (result == null)
            {
                throw new CustomApplicationException(ErrorCode.InvalidCredentials, "Bad credentials at login!");
            }
            result.Token = CreateToken(result.Email, result.Role);

            return result;
        }

        private string CreateToken(string email, string role) 
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken
            (
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])),
                    SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}