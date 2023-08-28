using System.Collections.Generic;

namespace MealPlan.Business.Menus.Models
{
    public class GetMenuDetailsModel
    {
        public int Id { get; set; }
        public int Category { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public List<GetMenuMealModel> Meals { get; set; }
    }
}
