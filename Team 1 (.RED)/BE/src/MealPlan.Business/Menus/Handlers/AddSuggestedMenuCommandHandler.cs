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
    public class AddSuggestedMenuCommandHandler : IRequestHandler<AddSuggestedMenuCommand, bool>
    {
        private readonly MealPlanContext _context;

        public AddSuggestedMenuCommandHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddSuggestedMenuCommand command, CancellationToken cancellationToken)
        {
            List<Ingredient> newIngredients = new List<Ingredient>();

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
                    Recipe = new Recipe
                    {
                        Description = m.Recipe.Description,
                        Name = m.Recipe.Name,
                        Ingredients = m.Recipe.Ingredients
                            .Select(ingredientName => ChooseProperIngredientAsync(ingredientName, newIngredients).Result)
                            .ToList()
                    }
                })
                .ToList()
            };

            await _context.Menus.AddAsync(menu);

            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<Ingredient> GetIngredientByNameAsync(string ingredientName)
        {
            return await _context.Ingredients
                .Where(i => i.Name == ingredientName)
                .FirstOrDefaultAsync();
        }

        public async Task<Ingredient> ChooseProperIngredientAsync(string ingredientName, List<Ingredient> newIngredients)
        {
            Ingredient foundDatabaseIngredient = await GetIngredientByNameAsync(ingredientName);

            if (foundDatabaseIngredient == null)
            {
                Ingredient currentlyAddedIngredient = newIngredients
                    .Find(i => i.Name.Equals(ingredientName));

                if (currentlyAddedIngredient == null)
                {
                    Ingredient newIngredient = new Ingredient { Name = ingredientName };
                    newIngredients.Add(newIngredient);

                    return newIngredient;
                }

                return currentlyAddedIngredient;
            }

            return foundDatabaseIngredient;
        }
    }
}