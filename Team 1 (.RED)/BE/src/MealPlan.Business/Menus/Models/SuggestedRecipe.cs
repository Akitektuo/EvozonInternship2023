using System.Collections.Generic;

namespace MealPlan.Business.Menus.Models
{
    public class SuggestedRecipe
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
    }
}