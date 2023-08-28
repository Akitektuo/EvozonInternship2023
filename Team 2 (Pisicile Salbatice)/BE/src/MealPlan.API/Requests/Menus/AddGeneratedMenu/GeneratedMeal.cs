using FluentValidation;
using MealPlan.Data.Models.Meals;

namespace MealPlan.API.Requests.Menus.AddGeneratedMenu
{
    public class GeneratedMeal
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public MealType MealTypeId { get; set; }
        public GeneratedRecipe Recipe { get; set; } = new GeneratedRecipe();
    }

    public class GeneratedMealValidator : AbstractValidator<GeneratedMeal>
    {
        public GeneratedMealValidator()
        {
            RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1024);
            RuleFor(x => x.MealTypeId)
                .NotEmpty()
                .IsInEnum();
            RuleFor(x => x.Recipe)
                .SetValidator(new GeneratedRecipeValidator());
        }
    }
}