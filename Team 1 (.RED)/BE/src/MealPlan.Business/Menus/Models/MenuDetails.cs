using System.Collections.Generic;

namespace MealPlan.Business.Menus.Models
{
    public class MenuDetails
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public List<MealDetails> MealsDetails { get; set; }
    }
}