using MealPlan.Data.Models.Meals;
using System.Collections.Generic;

namespace MealPlan.Data.Models.Menus
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public MenuCategory CategoryId { get; set; }

        public MenuCategoryLookup Category { get; set; }
        public List<Meal> Meals { get; set; }
    }
}