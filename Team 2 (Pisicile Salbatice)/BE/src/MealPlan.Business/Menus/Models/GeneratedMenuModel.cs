using MealPlan.Data.Models.Meals;
using System.Collections.Generic;

namespace MealPlan.Business.Menus.Models
{
    public class GeneratedMenuModel
    {
        public string Name { get; set; }
        public MenuType MenuTypeId { get; set; }

        public List<GeneratedMealModel> Meals { get; set; }
    }
}
