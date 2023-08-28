using MealPlan.Business.Meals.Commands;
using MealPlan.Data.Models.Menus;
using MediatR;
using System.Collections.Generic;

namespace MealPlan.Business.Menus.Commands
{
    public class AddMenuCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public MenuCategory CategoryId { get; set; }

        public List<AddMealCommand> Meals { get; set; }
    }
}