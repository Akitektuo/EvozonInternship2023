using System.Collections.Generic;

namespace MealPlan.Data.Models.Recipes
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Recipe> Recipes { get; }
    }
}