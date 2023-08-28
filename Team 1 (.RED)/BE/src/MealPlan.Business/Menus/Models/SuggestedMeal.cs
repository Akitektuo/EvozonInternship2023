using MealPlan.Data.Models.Meals;

namespace MealPlan.Business.Menus.Models
{
    public class SuggestedMeal
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public MealType MealTypeId { get; set; }
        public SuggestedRecipe Recipe { get; set; }
    }
}