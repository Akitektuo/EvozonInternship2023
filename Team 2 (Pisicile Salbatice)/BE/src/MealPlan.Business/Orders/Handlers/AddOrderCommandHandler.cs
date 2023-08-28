using MealPlan.Business.Exceptions;
using MealPlan.Business.Orders.Commands;
using MealPlan.Data;
using MealPlan.Data.Models.Meals;
using MealPlan.Data.Models.Orders;
using MealPlan.Data.Models.Users;
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
            var user = await GetUser(request.UserEmail);
            var menu = await GetMenu(request.MenuId);

            CheckUserExistence(user);
            CheckMenuExistence(menu);

            var userBalance = user.Balance;
            var totalPrice = await GetTotalPrice(request);

            if (userBalance < totalPrice)
            {
                throw new CustomApplicationException(ErrorCode.InvalidUserWallet, "Non-Sufficient Funds");
            }

            var newOrder = new Order
            {
                MenuId = request.MenuId,
                MenuName = menu.Name,
                UserId = user.Id,
                UserEmail = request.UserEmail,
                PhoneNumber = request.PhoneNumber,
                ShippingAddress = request.ShippingAddress,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                TotalPrice = totalPrice,
                StatusId = Status.WaitingForApproval,

                User = user,
                Menu = menu
            };

            await _context.AddAsync(newOrder);
            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<float> GetTotalPrice(AddOrderCommand request)
        {
            float menuPrice = await GetMenuPrice(request.MenuId);
            return (float)((request.EndDate - request.StartDate).Days + 1) * menuPrice;
        }

        private async Task<float> GetMenuPrice(int menuId)
        {
            return await _context.Meals
                .Where(meal => meal.MenuId == menuId)
                .Select(meal => meal.Price)
                .SumAsync();
        }

        private async Task<User> GetUser(string userEmail)
        {
            return await _context.Users
                .Where(user => user.Email == userEmail)
                .SingleOrDefaultAsync();
        }

        private async Task<Menu> GetMenu(int menuId)
        {
            return await _context.Menus
                .Where(menu => menu.Id == menuId)
                .SingleOrDefaultAsync();
        }

        private void CheckUserExistence(User user)
        {
            if (user == null)
            {
                throw new CustomApplicationException(ErrorCode.AddOrderUserNotFound, "User not found");
            }
        }

        private void CheckMenuExistence(Menu menu)
        {
            if (menu == null)
            {
                throw new CustomApplicationException(ErrorCode.AddOrderMenuNotFound, "Selected menu does not exist");
            }
        }
    }
}
