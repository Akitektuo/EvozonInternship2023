using MealPlan.Data.Models.Meals;
using System.Collections.Generic;

namespace MealPlan.Data.Models.Recipes
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Ingredient> Ingredients { get; set; }
        public Meal Meal { get; set; }
    }
}
