using MealPlan.Business.Menus.Models;
using MealPlan.Data.Models.Meals;
using System.Collections.Generic;
using System.Linq;

namespace MealPlan.Business.Meals
{
    public static class MealExtensions
    {
        public static IEnumerable<Meal> ToMeals(this IEnumerable<MealModel> mealModels)
        {
            return mealModels.Select(mealModel => new Meal
            {
                Name = mealModel.Name,
                Description = mealModel.Description.Trim(),
                Price = mealModel.Price,
                MealTypeId = (MealType)mealModel.MealTypeId,
                RecipeId = mealModel.RecipeId,
            });
        }
    }
}
