using MealPlan.Business.Users.Commands;
using MealPlan.Data;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using MealPlan.Data.Models.Users;
using MealPlan.Business.Exceptions;

namespace MealPlan.Business.Users.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, bool>
    {
        private readonly MealPlanContext _context;

        public RegisterCommandHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Users.AnyAsync(r => r.Email == request.Email))
            {
                throw new CustomApplicationException(ErrorCode.UserAlreadyExists, "");
            }

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                RoleId = Role.User
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
