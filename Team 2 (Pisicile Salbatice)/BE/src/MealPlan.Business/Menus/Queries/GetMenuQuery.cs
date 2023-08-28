using MealPlan.Business.Menus.Models;
using MediatR;

namespace MealPlan.Business.Menus.Queries
{
    public class GetMenuQuery : IRequest<GetMenuDetailsModel>
    {
        public int Id { get; set; }
    }
}
