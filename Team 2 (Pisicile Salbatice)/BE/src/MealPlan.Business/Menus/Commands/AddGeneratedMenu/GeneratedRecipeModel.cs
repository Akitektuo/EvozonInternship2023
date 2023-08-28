using System.Collections.Generic;

namespace MealPlan.Business.Menus.Commands.AddGeneratedMenu
{
    public class GeneratedRecipeModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; }
    }
}