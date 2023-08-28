using MealPlan.Data.Models.Meals;
using System.Collections.Generic;

namespace MealPlan.Business.Menus.Models
{
    public class MealDetails
    {
        public string Name { get; set; }
        public MealType MealTypeId { get; set; }

        public RecipeDetails RecipeDetails { get; set; }
    }
}