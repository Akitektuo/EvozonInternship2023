using MealPlan.Business.Recipes.Models;
using MediatR;

namespace MealPlan.Business.Recipes.Queries
{
    public class GetRecipesQuery : IRequest<GetRecipesModel>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool FilterUnusedRecipes { get; set; }
    }
}
