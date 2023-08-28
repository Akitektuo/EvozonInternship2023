using MediatR;
using System;

namespace MealPlan.Business.Orders.Commands
{
    public class AddOrderCommand : IRequest<bool>
    {
        public string UserEmail { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MenuId { get; set; }
    }
}