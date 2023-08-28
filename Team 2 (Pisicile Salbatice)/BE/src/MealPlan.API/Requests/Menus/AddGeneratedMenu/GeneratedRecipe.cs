using FluentValidation;
using System.Collections.Generic;

namespace MealPlan.API.Requests.Menus.AddGeneratedMenu
{
    public class GeneratedRecipe
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; }
    }

    public class GeneratedRecipeValidator : AbstractValidator<GeneratedRecipe>
    {
        public GeneratedRecipeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(500);
            RuleFor(x => x.Ingredients)
                .NotEmpty();
        }
    }
}