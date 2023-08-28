using System.Collections.Generic;

namespace MealPlan.Business.Recipes.Models
{
    public record RecipeDetails
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; }
    }
}