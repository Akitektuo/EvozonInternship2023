using MealPlan.Business.Menus.Models;
using MealPlan.Data.Models.Meals;
using MealPlan.Data.Models.Menus;
using MealPlan.Data.Models.Recipes;
using System.Collections.Generic;
using System.Linq;

namespace MealPlan.Business.Menus
{
    public static class MenuExtensions
    {
        public static IQueryable<MenuDetails> ToMenuDetails(this IQueryable<Menu> query)
        {
            return query.Select(q => new MenuDetails
            {
                Name = q.Name,
                Description = q.Description,
                MealsDetails = GetMealDetails(q.Meals).ToList(),
                Price = (int)q.Meals.Sum(m => m.Price)
            });
        }

        public static IQueryable<MealDetails> GetMealDetails(List<Meal> meals)
        {
            return meals
                .Select(q => new MealDetails
                {
                    Name = q.Name,
                    MealTypeId = q.MealTypeId,
                    RecipeDetails = GetRecipeDetails(q.Recipe)
                }).AsQueryable();
        }

        private static RecipeDetails GetRecipeDetails(Recipe recipe)
        {
            return new RecipeDetails
            {
                Description = recipe.Description,
                Ingredients = recipe.Ingredients.Select(ingredient => ingredient.Name).ToList()
            };
        }
    }
}