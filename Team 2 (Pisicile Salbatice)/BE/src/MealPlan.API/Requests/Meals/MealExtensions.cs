using MealPlan.API.Requests.Menus;
using MealPlan.Business.Menus.Models;
using System.Collections.Generic;
using System.Linq;

namespace MealPlan.API.Requests.Meals
{
    public static class MealExtensions
    {
        public static IEnumerable<MealModel> ToMealModels(this IEnumerable<MealReceived> meals)
        {
            return meals.Select(x => new MealModel
            {
                Description = x.Description,
                MealTypeId = x.MealTypeId,
                Name = x.Name,
                Price = x.Price,
                RecipeId = x.RecipeId
            });
        }
    }
}
