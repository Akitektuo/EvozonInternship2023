using MealPlan.Business.Exceptions;
using MealPlan.Business.Recipes.Models;
using MealPlan.Business.Recipes.Queries;
using MealPlan.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.Business.Recipes.Handlers
{
    public class GetRecipeHandler : IRequestHandler<GetRecipeQuery, RecipeModel>
    {
        private readonly MealPlanContext _context;

        public GetRecipeHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<RecipeModel> Handle(GetRecipeQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Recipes
                .Where(x => x.Id == request.RecipeId)
                .ToRecipeModel()
                .SingleOrDefaultAsync();

            if (result == null)
            {
                throw new CustomApplicationException(ErrorCode.RecipeNotFound, "The recipe does not exist.");
            }

            return result;
        }
    }
}
