using MealPlan.Business.Exceptions;
using MealPlan.Business.Meals;
using MealPlan.Business.Menus.Commands;
using MealPlan.Data;
using MealPlan.Data.Models.Meals;
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

        public async Task<bool> Handle(AddMenuCommand request, CancellationToken cancellationToken)
        {
            await ValidateMenuAndMeals(request);

            var newMeals = request.Meals.ToMeals().ToList();
            var newMenu = new Menu { Name = request.MenuName, MenuTypeId = (MenuType)request.MenuTypeId, Meals = newMeals };

            await _context.AddAsync(newMenu);
            await _context.SaveChangesAsync();

            return true;
        }

        private async Task ValidateMenuAndMeals(AddMenuCommand request)
        {
            var mealNameDuplicates = await GetMealNamesDuplicates(request);

            if (mealNameDuplicates.Count != 0)
            {
                throw new CustomApplicationException(
                    ErrorCode.MealNameDuplicated,
                    $"The following meal names are already used: {string.Join(", ", mealNameDuplicates.ToArray())}");
            }

            var existingRecipes = await GetExistingRecipesIds(request);

            if (existingRecipes.Count != request.Meals.Count)
            {
                throw new CustomApplicationException(
                    ErrorCode.RecipeNotFound,
                    "Recipe for meal not found.");
            }

            var menuNameDuplicate = await _context.Menus.AnyAsync(x => request.MenuName == x.Name);

            if (menuNameDuplicate)
            {
                throw new CustomApplicationException(
                    ErrorCode.MenuNameDuplicated,
                    $"A menu with name {request.MenuName} already exists.");
            }

            var recipeIdsAlreadyUsed = await GetRecipeIdsAssignedToMeals(request);

            if (recipeIdsAlreadyUsed.Count != 0)
            {
                var recipesUsedNames = _context.Recipes.Where(x => recipeIdsAlreadyUsed.Any(r => r == x.Id)).Select(m => m.Name).ToList();

                throw new CustomApplicationException(
                    ErrorCode.RecipeAlredyUsed,
                    $"The following recipes are already used: {string.Join(", ", recipesUsedNames.ToArray())}");
            }
        }

        private async Task<List<string>> GetMealNamesDuplicates(AddMenuCommand request)
        {
            var meals = await _context.Meals
                 .Select(x => x.Name.ToLower())
                 .ToListAsync();

            return meals.Intersect(request.Meals.Select(m => m.Name.ToLower()))
                .ToList();
        }

        private async Task<List<int>> GetRecipeIdsAssignedToMeals(AddMenuCommand request)
        {
            var recipeIds = await _context.Meals.Select(x => x.RecipeId)
                .ToListAsync();

            return recipeIds.Intersect(request.Meals.Select(m => m.RecipeId))
                .ToList();
        }

        private async Task<List<int>> GetExistingRecipesIds(AddMenuCommand request)
        {
            var recipeIds = await _context.Recipes.Select(x => x.Id)
                .ToListAsync();

            return recipeIds.Intersect(request.Meals.Select(m => m.RecipeId))
                .ToList();
        }
    }
}
