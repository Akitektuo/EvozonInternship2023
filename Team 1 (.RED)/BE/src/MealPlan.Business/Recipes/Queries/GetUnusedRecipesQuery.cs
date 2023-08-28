using MealPlan.Business.Recipes.Models;
using MealPlan.Business.Utils;
using MediatR;

namespace MealPlan.Business.Recipes.Queries
{
    public class GetUnusedRecipesQuery : IRequest<PaginationModel<RecipeOverview>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}