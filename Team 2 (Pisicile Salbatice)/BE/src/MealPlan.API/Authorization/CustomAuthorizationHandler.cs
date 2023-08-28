using MealPlan.API.Requests.Users;
using MealPlan.API.Services.UserIdentity;
using MealPlan.Data.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using IAuthorizationService = MealPlan.API.Services.Authorization.IAuthorizationService;

namespace MealPlan.API.Authorization
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params Role[] roles)
        {
            Roles = string.Join(',', roles.Select(r => r.ToString()).ToList());
        }
    }

    public class CustomAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserIdentityService _userIdentityService;

        public CustomAuthorizationHandler(IAuthorizationService authorizationService, IUserIdentityService userIdentityService)
        {
            _authorizationService = authorizationService;
            _userIdentityService = userIdentityService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesAuthorizationRequirement requirement)
        {
            var email = _userIdentityService.GetEmailClaim();

            if (string.IsNullOrEmpty(email))
            {
                context.Fail();
                return;
            }

            if (requirement.AllowedRoles.Count() == 0)
            {
                context.Succeed(requirement);
                return;
            }

            var model = new GetUserRoleRequest
            {
                Role = requirement.AllowedRoles.SingleOrDefault(),
                Email = email
            };

            var isAuthorized = await _authorizationService.Authorize(model);

            if (!isAuthorized)
            {
                context.Fail();
            }

            context.Succeed(requirement);
        }
    }
}
