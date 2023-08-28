using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace MealPlan.API.Requests.Recipes
{
    public class AddSuggestedRecipeRequest
    {
        public string Description { get; set; }
        public string Name { get; set; }

        public List<string> Ingredients { get; set; }
    }

    public class AddSuggestedRecipeRequestValidator : AbstractValidator<AddSuggestedRecipeRequest>
    {
        public AddSuggestedRecipeRequestValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(250);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50);

            RuleFor(x => x.Ingredients)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(ingredientList => ingredientList.TrueForAll(i => !i.IsNullOrEmpty()));
        }
    }
}