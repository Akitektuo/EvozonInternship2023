using System.Collections.Generic;

namespace MealPlan.Business.Recipes.Models
{
    public class GetRecipesModel
    {
        public List<RecipeResponse> RecipesList { get; set; }
        public int TotalRecipesCount { get; set; }
    }
}
