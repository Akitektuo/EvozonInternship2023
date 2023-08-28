using MealPlan.API.Requests.Users;
using MealPlan.Data.Models.Users;
using MediatR;
using System.Threading.Tasks;

namespace MealPlan.API.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IMediator _mediator;

        public AuthorizationService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Authorize(GetUserRoleRequest request)
        {
            var role = await _mediator.Send(request.ToQuery());

            return role.ToString() == request.Role;
        }
    }
}
