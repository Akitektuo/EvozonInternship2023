using MediatR;

namespace MealPlan.Business.Orders.Commands
{
    public class ApproveOrderCommand : IRequest<bool>
    {
        public int OrderID { get; set; }
    }
}