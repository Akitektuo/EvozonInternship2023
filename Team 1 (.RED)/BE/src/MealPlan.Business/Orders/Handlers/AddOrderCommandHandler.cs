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
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, bool>
    {
        private readonly MealPlanContext _context;

        public AddOrderCommandHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            int userId = await GetUserIdAsync(request.UserEmail);

            if (userId == 0)
            {
                throw new CustomApplicationException(ErrorCode.OrderUserNotFound, "User not found");
            }

            if (!await MenuExistsAsync(request.MenuId))
            {
                throw new CustomApplicationException(ErrorCode.OrderMenuNotFound, "Menu not found");
            }

            var order = new Order
            {
                UserId = userId,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                MenuId = request.MenuId
            };

            await _context.Orders.AddAsync(order);

            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<int> GetUserIdAsync(string userEmail)
        {
            return await _context.Users
                .Where(user => user.Email.Equals(userEmail))
                .Select(user => user.Id)
                .SingleOrDefaultAsync();
        }

        private async Task<bool> MenuExistsAsync(int menuId)
        {
            return await _context.Menus
                .AnyAsync(menu => menu.Id == menuId);
        }
    }
}