using MealPlan.Business.Menus.Models;
using MealPlan.Data.Models.Meals;
using MediatR;
using System.Collections.Generic;

namespace MealPlan.Business.Menus.Commands
{
    public class AddMenuCommand : IRequest<bool>
    {
        public string MenuName { get; set; }
        public MenuType MenuTypeId { get; set; }
        public List<MealModel> Meals { get; set; }
    }
}
