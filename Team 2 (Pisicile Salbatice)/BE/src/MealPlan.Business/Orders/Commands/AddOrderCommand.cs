using MediatR;
using System;

namespace MealPlan.Business.Orders.Commands
{
    public class AddOrderCommand: IRequest<bool>
    {
        public int MenuId { get; set; }
        public string UserEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
