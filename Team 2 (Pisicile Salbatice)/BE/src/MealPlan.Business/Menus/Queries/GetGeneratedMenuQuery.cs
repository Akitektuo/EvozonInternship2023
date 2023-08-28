using MealPlan.Business.Menus.Models;
using MealPlan.Data.Models.Meals;
using MediatR;

namespace MealPlan.Business.Menus.Queries
{
    public class GetGeneratedMenuQuery : IRequest<GeneratedMenuModel>
    {
        public MenuType MenuType { get; set; }
        public float PriceSuggestion { get; set; }
    }
}
