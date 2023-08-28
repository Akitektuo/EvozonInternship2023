using MealPlan.Data.Models.Orders;
using MediatR;

namespace MealPlan.Business.Orders.Commands
{
    public class UpdateOrderStatusCommand : IRequest<bool>
    {
        public int OrderId { get; set; }
        public Status StatusId { get; set; }
    }
}
