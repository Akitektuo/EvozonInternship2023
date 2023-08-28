using System.Collections.Generic;

namespace MealPlan.Business.Menus.Models
{
    public class GetAllMenusModel
    {
        public List<MenuModel> MenusList { get; set; }
        public int TotalMenusCount { get; set; }
    }
}
