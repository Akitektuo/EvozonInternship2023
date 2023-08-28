using MealPlan.Business.Exceptions;
using MealPlan.Business.Orders.Commands;
using MealPlan.Data;
using MealPlan.Data.Models.Orders;
using MealPlan.Data.Models.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.Business.Orders.Handlers
{
    public class ApproveOrderCommandHandler : IRequestHandler<ApproveOrderCommand, bool>
    {
        private readonly MealPlanContext _context;

        public ApproveOrderCommandHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ApproveOrderCommand request, CancellationToken cancellationToken)
        {
            if (OrderDoesNotExist(request))
            {
                throw new CustomApplicationException(ErrorCode.OrderDoesNotExist, "Order does not exist");
            }

            if (OrderAlreadyApproved(request))
            {
                throw new CustomApplicationException(ErrorCode.OrderAlreadyApproved, "Order has already been approved");
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

            await ApproveCommand(request, order);

            return await _context.SaveChangesAsync() == 2;
        }

        private bool OrderDoesNotExist(ApproveOrderCommand request)
        {
            return !_context.Orders.Where(x => x.Id == request.OrderID).Any();
        }

        private bool OrderAlreadyApproved(ApproveOrderCommand command)
        {
            return _context.Orders.Where(x => x.Id == command.OrderID && x.OrderStatusId.Equals(OrderStatus.Approved)).Any();
        }

        private bool OrderStartDateAlreadyPassed(ApproveOrderCommand command)
        {
            return _context.Orders.Where(x => x.Id == command.OrderID && x.StartDate < DateTime.Today.AddDays(1)).Any();
        }

        private async Task ApproveCommand(ApproveOrderCommand request, Order order)
        {
            var menuPrice = await GetMenuPrice(request);

            var numberOfDays = await OrderNumberOfDays(request);

            var totalPrice = menuPrice * numberOfDays;

            if (UserDoesNotHaveEnoughMoney(request, totalPrice))
            {
                UpdateApprovalStatus(order, OrderStatus.Denied);

                await _context.SaveChangesAsync();

                throw new CustomApplicationException(ErrorCode.UserDoesNotHaveEnoughMoney, "User doesn't have enough money to purchase");
            }

            if (OrderStartDateAlreadyPassed(request))
            {
                UpdateApprovalStatus(order, OrderStatus.Denied);

                await _context.SaveChangesAsync();

                throw new CustomApplicationException(ErrorCode.OrderStartDatePassed, "Order start date has already passed");
            }

            UpdateApprovalStatus(order, OrderStatus.Approved);

            UpdateWalletBalance(order.User, totalPrice);
        }

        private async Task<double> GetMenuPrice(ApproveOrderCommand request)
        {
            return await _context.Orders.Where(x => x.Id == request.OrderID).Select(x => x.Menu.Meals.Sum(x => x.Price)).SingleOrDefaultAsync();
        }

        private bool UserDoesNotHaveEnoughMoney(ApproveOrderCommand request, double totalPrice)
        {
            return !_context.Orders.Where(x => x.Id == request.OrderID && x.User.WalletBalance - totalPrice > 0).Any();
        }

        private async Task<double> OrderNumberOfDays(ApproveOrderCommand request)
        {
            return await _context.Orders.Where(x => x.Id == request.OrderID).Select(x => (x.EndDate - x.StartDate).TotalDays + 1).FirstAsync();
        }

        private void UpdateApprovalStatus(Order order, OrderStatus status)
        {
            order.OrderStatusId = status;

            _context.Update(order);
        }

        private void UpdateWalletBalance(User user, double totalPrice)
        {
            user.WalletBalance -= totalPrice;

            _context.Update(user);
        }
    }
}