using MealPlan.Data.Models.Recipes;

namespace MealPlan.Data.Models.Meals
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public MealType MealTypeId { get; set; }
        public int RecipeId { get; set; }
        public int MenuId { get; set; }

        public MealTypeLookup MealType { get; set; }
        public Recipe Recipe { get; set; }
        public Menu Menu { get; set; }
    }
}