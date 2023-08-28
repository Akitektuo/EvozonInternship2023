using MealPlan.Business.Exceptions;
using MealPlan.Data;
using MealPlan.Data.Models.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlan.Business.Services
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly MealPlanContext _context;

        public OrderStatusService(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<bool> ApproveOrder(int orderId)
        {
            var order = await _context.Orders
                .Where(o => o.Id == orderId)
                .FirstOrDefaultAsync();

            if (order is null)
            {
                throw new CustomApplicationException(
                    ErrorCode.OrderDoesNotExist,
                    $"Order with id '{orderId}' does not exist");
            }

            var result = order.StatusId switch
            {
                Status.WaitingForApproval => await ValidApproval(order),
                Status.Rejected => await ValidApproval(order),
                _ => throw new CustomApplicationException(
                    ErrorCode.CannotApproveOrder,
                    $"Cannot approve order with id '{orderId}'")
            };

            return result;
        }

        private async Task<bool> ValidApproval(Order order)
        {
            var user = await _context.Users
                .Where(u => u.Id == order.UserId)
                .SingleOrDefaultAsync();

            if (user.Balance < order.TotalPrice)
            {
                throw new CustomApplicationException(
                    ErrorCode.InvalidUserWallet,
                    "User does not have enough currency");
            }

            var dateDifference = order.StartDate.Subtract(DateTime.Now);

            if (dateDifference.Days < 1)
            {
                order.StatusId = Status.Rejected;
                await _context.SaveChangesAsync();

                throw new CustomApplicationException(
                    ErrorCode.OrderIsOutdated, 
                    $"Order with id '{order.Id}' is outdated since '{order.StartDate}'");
            }

            user.Balance = user.Balance - order.TotalPrice;
            order.StatusId = Status.Approved;

            var result = await _context.SaveChangesAsync();
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RejectOrder(int orderId)
        {
            var order = await _context.Orders
                .Where(o => o.Id == orderId)
                .FirstOrDefaultAsync();

            if (order is null)
            {
                throw new CustomApplicationException(
                    ErrorCode.OrderDoesNotExist,
                    $"Order with id '{orderId}' does not exist");
            }

            var result = order.StatusId switch
            {
                Status.WaitingForApproval => await ValidRejection(order),
                _ => throw new CustomApplicationException(
                    ErrorCode.CannotRejectOrder,
                    $"Cannot reject order with id '{orderId}'")
            };

            return result;
        }

        private async Task<bool> ValidRejection(Order order)
        {
            order.StatusId = Status.Rejected;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
