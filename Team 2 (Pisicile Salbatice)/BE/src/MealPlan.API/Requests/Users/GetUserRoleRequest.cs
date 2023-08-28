using FluentValidation;
using MealPlan.API.Requests.Shared;
using MealPlan.Data.Models.Users;

namespace MealPlan.API.Requests.Users
{
    public class GetUserRoleRequest
    {
        public string Role { get; set; }
        public string Email { get; set; }
    }

    public class GetUserRoleValidator : AbstractValidator<LoginRequest>
    {
        public GetUserRoleValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .Matches(RegexConstants.Email);
        }
    }
}
