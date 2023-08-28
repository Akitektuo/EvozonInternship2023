using System.Collections.Generic;

namespace MealPlan.Business.Recipes.Models
{
    public class RecipeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; }
    }
}
