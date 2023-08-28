 using MealPlan.Data.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlan.API.Middleware
{
    public class RolesAuthAttribute : AuthorizeAttribute
    {
        public RolesAuthAttribute(params Role[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }

    public class UserRequirementHandler : AuthorizationHandler<RolesAuthorizationRequirement>
    {
        private readonly Services.IAuthorizationService _authorizationService;

        public UserRequirementHandler(Services.IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesAuthorizationRequirement requirement)
        {
            var email = context.User.Claims.Where(x => x.Type == "Email").ToString();

            if (email.IsNullOrEmpty())
            {
                context.Fail();
                return;
            }

            var allowedRoles = requirement.AllowedRoles.Select(x =>
            {
                Role role;
                Enum.TryParse<Role>(x, out role);
                return role;
            })
            .ToList();

            var isAuthorized = await _authorizationService.IsAuthorized(email, allowedRoles);

            if (!isAuthorized)
            {
                context.Fail();
                return;
            }

            context.Succeed(requirement);
        }
    }
}