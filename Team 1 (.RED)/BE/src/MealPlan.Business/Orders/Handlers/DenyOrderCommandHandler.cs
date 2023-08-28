using MealPlan.Business.Exceptions;
using MealPlan.Business.Orders.Commands;
using MealPlan.Data;
using MealPlan.Data.Models.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.Business.Orders.Handlers
{
    public class DenyOrderCommandHandler : IRequestHandler<DenyOrderCommand, bool>
    {
        private MealPlanContext _context;
        
        public DenyOrderCommandHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DenyOrderCommand request, CancellationToken cancellationToken)
        {
            if (OrderDoesntExist(request))
            {
                throw new CustomApplicationException(ErrorCode.OrderDoesNotExist, "Order doesn't exist");
            }

            if (OrderAlreadyApproved(request))
            {
                throw new CustomApplicationException(ErrorCode.OrderAlreadyApproved, "Order is already approved");
            }

            if (OrderAlreadyDenied(request))
            {
                throw new CustomApplicationException(ErrorCode.OrderAlreadyDenied, "Order is already denied");
            }

            var order = await _context.Orders
                .Where(x => x.Id == request.OrderID)
                .Select(x => new Order
                {
                    Id = x.Id,
                    Address = x.Address,
                    PhoneNumber = x.PhoneNumber,
                    Menu = x.Menu,
                    MenuId = x.MenuId,
                    User = x.User,
                    UserId = x.UserId,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    OrderStatusId = x.OrderStatusId
                })
                .SingleOrDefaultAsync();

            order.OrderStatusId = OrderStatus.Denied;

            _context.Update(order);
           
            return await _context.SaveChangesAsync() == 1;
        }

        private bool OrderAlreadyDenied(DenyOrderCommand request)
        {
            return _context.Orders.Where(x => x.Id == request.OrderID && x.OrderStatusId == OrderStatus.Denied).Any();
        }

        private bool OrderAlreadyApproved(DenyOrderCommand request)
        {
            return _context.Orders.Where(x => x.Id == request.OrderID && x.OrderStatusId == OrderStatus.Approved).Any();
        }

        private bool OrderDoesntExist(DenyOrderCommand request)
        {
            return !_context.Orders.Where(x => x.Id == request.OrderID).Any();
        }
    }
}