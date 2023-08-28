using MealPlan.Business.Exceptions;
using MealPlan.Business.Recipes.Commands;
using MealPlan.Data;
using MealPlan.Data.Models.Recipes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.Business.Recipes.Handlers
{
    public class AddRecipeCommandHandler : IRequestHandler<AddRecipeCommand, bool>
    {
        private readonly MealPlanContext _context;

        public AddRecipeCommandHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddRecipeCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Recipes.AnyAsync(r => r.Name == request.Name))
            {
                throw new CustomApplicationException(ErrorCode.RecipeAlreadyExists, $"Recipe with name '{request.Name}' already exists.");
            }

            var ingredients = await _context.Ingredients.Where(i => request.IngredientIds.Contains(i.Id)).ToListAsync();

            if (request.IngredientIds.All(i => ingredients.Select(x => x.Id).Contains(i)) == false)
            {
                throw new CustomApplicationException(ErrorCode.IngredientDoesNotExist, $"Ingredients list is not valid.");
            }

            var recipe = new Recipe
            {
                Name = request.Name,
                Description = request.Description,
                Ingredients = ingredients
            };

            await _context.AddAsync(recipe);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
