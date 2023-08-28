using FluentValidation;
using MealPlan.API.Requests.Utils;

namespace MealPlan.API.Requests.Recipes
{
    public class GetUnusedRecipesRequest : PageRequest
    {
    }

    public class GetUnusedRecipesRequestValidator : AbstractValidator<GetUnusedRecipesRequest>
    {
        public GetUnusedRecipesRequestValidator() 
        {
            Include(new PageRequestValidator());
        }
    }
}