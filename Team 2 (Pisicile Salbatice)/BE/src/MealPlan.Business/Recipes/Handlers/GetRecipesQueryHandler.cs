using MealPlan.Business.Recipes.Models;
using MealPlan.Business.Recipes.Queries;
using MealPlan.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.Business.Recipes.Handlers
{
    public class GetRecipesQueryHandler : IRequestHandler<GetRecipesQuery, GetRecipesModel>
    {
        private readonly MealPlanContext _context;

        public GetRecipesQueryHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<GetRecipesModel> Handle(GetRecipesQuery request, CancellationToken cancellationToken)
        {
            return new GetRecipesModel
            {
                RecipesList = await GetRecipes(request),
                TotalRecipesCount = await GetTotalRecipesCount(request.FilterUnusedRecipes)
            };
        }

        private async Task<List<RecipeResponse>> GetRecipes(GetRecipesQuery request)
        {
            return await _context.Recipes
                .Where(recipe => !request.FilterUnusedRecipes || recipe.Meal == null)
                .ToRecipeResponse()
                .OrderBy(recipe => recipe.Name)
                .Pagination(request.PageNumber, request.PageSize)
                .ToListAsync();
        }

        private async Task<int> GetTotalRecipesCount(bool filterUnusedRecipes)
        {
            return await _context.Recipes.CountAsync(recipe => !filterUnusedRecipes || recipe.Meal == null);
        }
    }
}
