using MealPlan.Data.Models.Meals;

namespace MealPlan.Business.Menus.Models
{
    public class MealModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public MealType MealTypeId { get; set; }
        public int RecipeId { get; set; }
    }
}
