using MealPlan.Business.Orders.Commands;
using MealPlan.Business.Orders.Models;
using MealPlan.Business.Orders.Queries;
using MealPlan.Business.Shared;
using System.Collections.Generic;
using System.Linq;
using MealPlan.API.Requests.Shared;

namespace MealPlan.API.Requests.Orders
{
    public static class OrderExtensions
    {
        public static UpdateOrderStatusCommand ToCommand(this UpdateOrderStatusRequest request)
        {
            return new UpdateOrderStatusCommand
            {
                StatusId = request.StatusId,
                OrderId = request.OrderId
            };
        }
        
        public static GetOrdersQuery ToQuery(this GetOrdersRequest request)
        {
            return new GetOrdersQuery
            {
                FiltrationModel = request.Filtration.ToBusinessFiltrationModel(),
                PageNumber = request.Pagination.PageNumber,
                PageSize = request.Pagination.PageSize
            };
        }

        public static AddOrderCommand ToCommand(this AddOrderRequest request)
        {
            return new AddOrderCommand
            {
                MenuId = request.MenuId,
                PhoneNumber = request.PhoneNumber,
                ShippingAddress = request.ShippingAddress,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                UserEmail = request.Email
            };
        }
    }
}
