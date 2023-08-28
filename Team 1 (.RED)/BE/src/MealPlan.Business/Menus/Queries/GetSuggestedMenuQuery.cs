using MealPlan.Business.Menus.Models;
using MealPlan.Data.Models.Menus;
using MediatR;

namespace MealPlan.Business.Menus.Queries
{
    public class GetSuggestedMenuQuery : IRequest<SuggestedMenu>
    {
        public MenuCategory CategoryId { get; set; }
        public double PriceSuggestion { get; set; }
    }
}