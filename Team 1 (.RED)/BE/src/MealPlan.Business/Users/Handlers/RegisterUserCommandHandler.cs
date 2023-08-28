using MealPlan.Business.Exceptions;
using MealPlan.Business.Users.Commands;
using MealPlan.Data;
using MealPlan.Data.Models.Users;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.Business.Users.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly MealPlanContext _context;

        public RegisterUserCommandHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            if (EmailAlreadyExists(command))
            {
                throw new CustomApplicationException(ErrorCode.RegistrationEmailAlreadyUsed, "Registration failed");
            }

            var user = new User
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                Password = command.Password
            };

            await _context.Users.AddAsync(user);

            return await _context.SaveChangesAsync() > 0;
        }

        private bool EmailAlreadyExists(RegisterUserCommand command)
        {
            return _context.Users.Any(x => x.Email == command.Email);
        }
    }
}