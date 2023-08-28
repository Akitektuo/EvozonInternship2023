using MealPlan.Business.Recipes.Models;
using MealPlan.Business.Recipes.Queries;
using MealPlan.Business.Utils;
using MealPlan.Data;
using MealPlan.Data.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.Business.Recipes.Handlers
{
    public class GetUnusedRecipesQueryHandler : IRequestHandler<GetUnusedRecipesQuery, PaginationModel<RecipeOverview>>
    {
        private readonly MealPlanContext _context;

        public GetUnusedRecipesQueryHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<PaginationModel<RecipeOverview>> Handle(GetUnusedRecipesQuery request, CancellationToken cancellationToken)
        {
            var recipes = await _context.Recipes
                .Where(r => r.Meal == null)
                .ToRecipeOverview()
                .ToPage(request.PageNumber, request.PageSize)
                .ToListAsync(cancellationToken);

            var numberOfElements = _context.Recipes
                .Where(r => r.Meal == null)
                .Count();

            return new PaginationModel<RecipeOverview> { Items = recipes, TotalRecords = numberOfElements };
        }
    }
}