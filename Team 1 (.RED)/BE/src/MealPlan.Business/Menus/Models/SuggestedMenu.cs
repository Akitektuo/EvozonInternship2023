using MealPlan.Data.Models.Menus;
using System.Collections.Generic;

namespace MealPlan.Business.Menus.Models
{
    public class SuggestedMenu
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public MenuCategory CategoryId { get; set; }
        public double TotalPrice { get; set; }
        public List<SuggestedMeal> Meals { get; set; }
    }
}