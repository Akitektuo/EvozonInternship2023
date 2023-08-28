using FluentValidation;
using MealPlan.API.Requests.Shared;
using MealPlan.Data.Models.Meals;

namespace MealPlan.API.Requests.Menus
{
    public class MealReceived
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public MealType MealTypeId { get; set; }
        public int RecipeId { get; set; }
    }

    public class MealReceivedValidator : AbstractValidator<MealReceived>
    {
        public MealReceivedValidator()
        {
            RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(x => x.Name)
                .NotEmpty()
                .Matches(RegexConstants.Name)
                .MaximumLength(50);
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1024);
            RuleFor(x => x.RecipeId)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(x => x.MealTypeId)
                .NotEmpty()
                .IsInEnum();
        }
    }
}
