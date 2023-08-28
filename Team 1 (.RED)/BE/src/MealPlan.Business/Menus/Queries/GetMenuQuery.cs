using MealPlan.Business.Menus.Models;
using MediatR;

namespace MealPlan.Business.Menus.Queries
{
    public class GetMenuQuery : IRequest<MenuDetails>
    {
        public int Id { get; set; }
    }
}