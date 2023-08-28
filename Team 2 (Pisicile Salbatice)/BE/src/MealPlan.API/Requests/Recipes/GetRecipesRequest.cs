using FluentValidation;
using MealPlan.API.Requests.Shared;

namespace MealPlan.API.Requests.Recipes
{
    public class GetRecipesRequest
    {
        public PaginationModel PaginationModel { get; set; } = new PaginationModel();
        public bool FilterUnusedRecipes { get; set; } = false;
    }

    public class GetRecipesRequestValidator : AbstractValidator<GetRecipesRequest>
    {
        public GetRecipesRequestValidator()
        {
            RuleFor(x => x.PaginationModel).SetValidator(new PaginationModelValidator());
        }
    }
}
