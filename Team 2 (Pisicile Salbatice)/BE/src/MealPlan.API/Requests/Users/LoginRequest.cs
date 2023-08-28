using FluentValidation;
using MealPlan.API.Requests.Shared;

namespace MealPlan.API.Requests.Users
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .Matches(RegexConstants.Email)
                .MinimumLength(7)
                .MaximumLength(50);

            RuleFor(x => x.Password)
                .NotEmpty()
                .Matches(RegexConstants.Password)
                .MinimumLength(5)
                .MaximumLength(20);
        }
    }
}
