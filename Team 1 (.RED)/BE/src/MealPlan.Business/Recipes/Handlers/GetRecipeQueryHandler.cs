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
    public class GetRecipeQueryHandler : IRequestHandler<GetRecipeQuery, RecipeDetails>
    {
        private readonly MealPlanContext _context;

        public GetRecipeQueryHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<RecipeDetails> Handle(GetRecipeQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Recipes
                .Where(r => r.Id == request.Id)
                .ToRecipeDetails()
                .SingleOrDefaultAsync();

            if (result == null)
            {
                throw new CustomApplicationException(ErrorCode.RecipeNotFound, "Recipe not found");
            }

            return result;
        }
    }
}