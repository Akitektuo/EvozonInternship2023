using MealPlan.Data.Models.Meals;
using MediatR;
using System.Collections.Generic;

namespace MealPlan.Business.Menus.Commands.AddGeneratedMenu
{
    public class AddGeneratedMenuCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public MenuType MenuTypeId { get; set; }
        public List<GeneratedMealModel> Meals { get; set; }
    }
}
