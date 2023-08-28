using MealPlan.Business.Exceptions;
using MealPlan.Business.Users.Models;
using MealPlan.Business.Users.Queries;
using MealPlan.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.Business.Users.Handlers
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginModel>
    {
        private readonly MealPlanContext _context;

        public LoginQueryHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<LoginModel> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var loginModel = await _context.Users
                .Where(u => u.Email == request.Email && u.Password == request.Password)
                .ToLoginModel()
                .SingleOrDefaultAsync();

            if(loginModel == null)
            {
                throw new CustomApplicationException(ErrorCode.BadCredentials, "Invalid credentials.");
            }

            return loginModel;
        }
    }
}
