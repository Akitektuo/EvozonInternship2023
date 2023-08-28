using MealPlan.Data.Models.Menus;
using MealPlan.Data.Models.Recipes;

namespace MealPlan.Data.Models.Meals
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public MealType MealTypeId  { get; set; }
        public int RecipeId { get; set; }
        public int MenuId { get; set; } 

        public MealTypeLookup MealType { get; set; }
        public Recipe Recipe { get; set; }  
        public Menu Menu { get; set; }  
    }
}