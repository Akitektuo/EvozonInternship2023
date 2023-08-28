using System.Collections.Generic;

namespace MealPlan.Data.Models.Meals
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MenuType MenuTypeId { get; set; }

        public MenuTypeLookup MenuType { get; set; }
        public List<Meal> Meals { get; set; }
    }
}
