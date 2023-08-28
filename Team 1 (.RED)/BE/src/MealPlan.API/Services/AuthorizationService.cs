using MealPlan.Business.Users.Queries;
using MealPlan.Data.Models.Users;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MealPlan.API.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IMediator _mediator;

        public AuthorizationService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> IsAuthorized(string email, List<Role> roles)
        {
            var result = await _mediator.Send(new GetRoleQuery { Email = email});

            return roles.Contains(result);
        }
    }
}