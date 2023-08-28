using MealPlan.Business.Menus.Models;
using MealPlan.Business.Utils;
using MealPlan.Data.Models.Menus;
using MediatR;

namespace MealPlan.Business.Menus.Queries
{
    public class GetAllMenusQuery : IRequest<PaginationModel<MenuOverview>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public MenuCategory CategoryId { get; set; }
    }
}