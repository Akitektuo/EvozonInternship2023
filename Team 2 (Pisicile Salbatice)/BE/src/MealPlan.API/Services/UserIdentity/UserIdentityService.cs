using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace MealPlan.API.Services.UserIdentity
{
    public class UserIdentityService : IUserIdentityService
    {
        private readonly IHttpContextAccessor _httpContext;

        public UserIdentityService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public string GetEmailClaim()
        {
            var claim = _httpContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email, System.StringComparison.OrdinalIgnoreCase));

            return claim is null ? string.Empty : claim.Value;
        }
    }
}
