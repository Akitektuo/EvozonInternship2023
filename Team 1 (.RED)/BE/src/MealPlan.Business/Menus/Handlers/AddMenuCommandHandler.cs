using MealPlan.Business.Exceptions;
using MealPlan.Business.Menus.Commands;
using MealPlan.Data;
using MealPlan.Data.Models.Meals;
using MealPlan.Data.Models.Menus;
using MealPlan.Data.Models.Recipes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.Business.Menus.Handlers
{
    public class AddMenuCommandHandler : IRequestHandler<AddMenuCommand, bool>
    {
        private readonly MealPlanContext _context;

        public AddMenuCommandHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddMenuCommand command, CancellationToken cancellationToken)
        {
            List<Recipe> recipes = command.Meals.Select(m => GetRecipeById(m.RecipeId)).ToList();

            if (recipes.Contains(null))
            {
                throw new CustomApplicationException(ErrorCode.MealRecipeNotFound, "Recipe does not exist");
            }

            if (recipes.Any(r => r.Meal != null))
            {
                throw new CustomApplicationException(ErrorCode.MealRecipeAlreadyUsed, "Recipe already used");
            }

            var menu = new Menu
            {
                Name = command.Name,
                Description = command.Description,
                CategoryId = command.CategoryId,
                Meals = command.Meals.Select(m => new Meal
                    {
                        Name = m.Name,
                        Price = m.Price,
                        MealTypeId = m.MealTypeId,
                        RecipeId = m.RecipeId
                    })
                    .ToList()
            };

            await _context.Menus.AddAsync(menu);

            return await _context.SaveChangesAsync() > 0;
        }

        private Recipe GetRecipeById(int recipeId)
        {
            Recipe recipe = _context.Recipes
                .Include(recipe => recipe.Meal)
                .Where(recipe => recipe.Id == recipeId)
                .SingleOrDefault();

            return recipe;
        }
    }
}