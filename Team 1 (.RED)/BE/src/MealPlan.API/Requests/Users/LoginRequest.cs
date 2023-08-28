using FluentValidation;
using MealPlan.API.Utils;

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
            RuleFor(x => x.Password)
                .NotNull()
                .MinimumLength(5)
                .MaximumLength(20);

            RuleFor(x => x.Email)
                .NotNull()
                .MinimumLength(7)
                .MaximumLength(50)
                .Matches(RegexConstants.Email);
        }
    }
}