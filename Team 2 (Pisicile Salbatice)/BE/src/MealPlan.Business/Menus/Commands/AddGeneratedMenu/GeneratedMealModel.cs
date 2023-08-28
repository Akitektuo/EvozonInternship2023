using MealPlan.Data.Models.Meals;

namespace MealPlan.Business.Menus.Commands.AddGeneratedMenu
{
    public class GeneratedMealModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public MealType MealTypeId { get; set; }
        public GeneratedRecipeModel Recipe { get; set; }
    }
}