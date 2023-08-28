using MealPlan.Business.Exceptions;
using MealPlan.Business.Recipes.Commands;
using MealPlan.Data;
using MealPlan.Data.Models.Recipes;
using MealPlan.Business.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<bool> Handle(AddRecipeCommand command, CancellationToken cancellationToken)
        {
            if (!command.IngredientIds.Any())
            {
                throw new CustomApplicationException(ErrorCode.EmptyIngredientsList, "Ingredients list is empty");
            }

            var recipe = new Recipe
            {
                Name = command.Name,
                Description = command.Description,
                Ingredients = MapIngredients(command.IngredientIds).Result
            };

            if (recipe.Ingredients.Count != command.IngredientIds.Count)
            {
                throw new CustomApplicationException(ErrorCode.IngredientNotFound, "Some ingredients from the list were not found");
            }

            await _context.Recipes.AddAsync(recipe);

            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<List<Ingredient>> MapIngredients(List<int> ingredientIds)
        {
            List<Ingredient> ingredients = await _context.Ingredients
               .Where(ingredient => ingredientIds.Any(x => x == ingredient.Id))
               .ToListAsync();

            return ingredients;
        }
    }
}