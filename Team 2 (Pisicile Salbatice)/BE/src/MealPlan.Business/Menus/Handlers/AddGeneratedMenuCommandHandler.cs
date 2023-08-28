using MealPlan.Business.Exceptions;
using MealPlan.Business.Menus.Commands.AddGeneratedMenu;
using MealPlan.Data;
using MealPlan.Data.Models.Meals;
using MealPlan.Data.Models.Recipes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.Business.Menus.Handlers
{
    public class AddGeneratedMenuCommandHandler : IRequestHandler<AddGeneratedMenuCommand, bool>
    {
        private readonly MealPlanContext _context;

        public AddGeneratedMenuCommandHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddGeneratedMenuCommand request, CancellationToken cancellationToken)
        {
            await ValidateInput(request);

            var menu = await CreateMenu(request);

            await _context.AddAsync(menu);
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<Menu> CreateMenu(AddGeneratedMenuCommand request)
        {
            var mealsList = new List<Meal>();
            var ingredientsList = await AddIngredientsToContext(request);

            foreach (var mealModel in request.Meals)
            {
                var meal = mealModel.ToMeal();

                var newIngredients = MapRecipeIngredients(ingredientsList, meal.Recipe);
                meal.Recipe.Ingredients = newIngredients;

                mealsList.Add(meal);
            }

            return new Menu { Name = request.Name, MenuTypeId = request.MenuTypeId, Meals = mealsList };
        }

        private static List<Ingredient> MapRecipeIngredients(List<Ingredient> ingredientsList, Recipe recipe)
        {
            var newIngredients = new List<Ingredient>();

            foreach (var ingredient in recipe.Ingredients)
            {
                var newIngredient = ingredientsList.Where(x => x.Name == ingredient.Name).FirstOrDefault();

                newIngredients.Add(newIngredient);
            }

            return newIngredients;
        }

        private async Task<List<Ingredient>> AddIngredientsToContext(AddGeneratedMenuCommand request)
        {
            var ingredientsList = await _context.Ingredients.ToListAsync();

            foreach (var mealModel in request.Meals)
            {
                var meal = mealModel.ToMeal();
                var recipe = meal.Recipe;

                foreach(var ingredient in recipe.Ingredients)
                {
                    if (!ingredientsList.Select(x => x.Name).Contains(ingredient.Name))
                    {
                        ingredientsList.Add(ingredient);
                    }
                }
            }

            return ingredientsList;
        }

        private async Task ValidateInput(AddGeneratedMenuCommand request)
        {
            var menuNameDuplicate = await _context.Menus.AnyAsync(x => request.Name == x.Name);

            if (menuNameDuplicate)
            {
                throw new CustomApplicationException(
                    ErrorCode.MenuNameDuplicated,
                    $"A menu with name {request.Name} already exists.");
            }

            var mealNameDuplicates = await GetMealNamesDuplicates(request);

            if (mealNameDuplicates.Count != 0)
            {
                throw new CustomApplicationException(
                    ErrorCode.MealNameDuplicated,
                    $"The following meal names are already used: {string.Join(", ", mealNameDuplicates.ToArray())}");
            }

            var recipeNamesDuplicates = await GetRecipeNamesDuplicates(request);

            if (recipeNamesDuplicates.Count != 0)
            {
                throw new CustomApplicationException(
                    ErrorCode.RecipeAlredyUsed,
                    $"The following recipes are already used: {string.Join(", ", recipeNamesDuplicates.ToArray())}");
            }
        }

        private async Task<List<string>> GetMealNamesDuplicates(AddGeneratedMenuCommand request)
        {
            var meals = await _context.Meals
                 .Select(x => x.Name.ToLower())
                 .ToListAsync();

            return meals.Intersect(request.Meals.Select(m => m.Name.ToLower()))
                 .ToList();
        }

        private async Task<List<string>> GetRecipeNamesDuplicates(AddGeneratedMenuCommand request)
        {
            var recipes = await _context.Recipes.Select(x => x.Name.ToLower())
                .ToListAsync();

            return recipes.Intersect(request.Meals.Select(m => m.Recipe.Name.ToLower()))
                .ToList();
        }
    }

    public static class GeneratedMenuExtensions
    {
        public static Meal ToMeal(this GeneratedMealModel model)
        {
            return new Meal
            {
                MealTypeId = model.MealTypeId,
                Name = model.Name.Trim(),
                Description = model.Description.Trim(),
                Price = model.Price,
                Recipe = model.Recipe.ToRecipe()
            };
        }

        public static Recipe ToRecipe(this GeneratedRecipeModel model)
        {
            return new Recipe
            {
                Name = model.Name.Trim(),
                Description = model.Description.Trim(),
                Ingredients = model.Ingredients.MapIds()
            };
        }

        public static List<Ingredient> MapIds(this List<string> ingredients)
        {
            return ingredients
                .Select(ingredientName => new Ingredient
                {
                    Name = ingredientName.ToLower()
                })
                .ToList();
        }
    }
}
