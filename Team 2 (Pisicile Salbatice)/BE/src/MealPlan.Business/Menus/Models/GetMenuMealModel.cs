using System.Collections.Generic;

namespace MealPlan.Business.Menus.Models
{
    public class GetMenuMealModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int MealTypeId { get; set; }
        public List<string> Ingredients { get; set; }
    }
}
