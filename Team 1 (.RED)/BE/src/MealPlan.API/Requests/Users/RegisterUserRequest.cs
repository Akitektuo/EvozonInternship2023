using FluentValidation;
using MealPlan.API.Utils;

namespace MealPlan.API.Requests.Users
{
    public class RegisterUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator() 
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50)
                .Matches(RegexConstants.Name);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50)
                .Matches(RegexConstants.Name);

            RuleFor(x => x.Email)
                .NotNull()
                .MinimumLength(7)
                .MaximumLength(50)
                .Matches(RegexConstants.Email);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(20)
                .Matches(RegexConstants.PasswordNoTrailingSpaces);

            RuleFor(x => x.ConfirmPassword)
                .Must((model, confirmPassword) => Equals(model.Password, confirmPassword))
                .WithMessage("Password and Confirm Password must match");
        }
    }
}