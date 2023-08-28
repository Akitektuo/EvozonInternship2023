using MediatR;

namespace MealPlan.Business.Orders.Commands
{
    public class DenyOrderCommand : IRequest<bool>
    {
        public int OrderID { get; set; }
    }
}