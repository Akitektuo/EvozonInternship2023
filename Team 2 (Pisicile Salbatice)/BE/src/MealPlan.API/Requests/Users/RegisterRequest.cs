using FluentValidation;
using MealPlan.API.Requests.Shared;

namespace MealPlan.API.Requests.Users
{
    public class RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
    }

    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator() {
            RuleFor(x => x.FirstName)
                .Matches(RegexConstants.Name)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.LastName)
                .Matches(RegexConstants.Name)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Email)
                .NotEmpty()
                .MinimumLength(7)
                .MaximumLength(50)
                .Matches(RegexConstants.Email);
            RuleFor(x => x.Password)
                .NotEmpty()
                .Matches(RegexConstants.Password)
                .MinimumLength(5)
                .MaximumLength(20);
            RuleFor(x => x.ConfirmedPassword)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(20);
            RuleFor(x => x.ConfirmedPassword)
                .Equal(x => x.Password)
                .WithMessage($"{nameof(RegisterRequest.ConfirmedPassword)} value should be equal to {nameof(RegisterRequest.Password)} value");
        }
    }
}
