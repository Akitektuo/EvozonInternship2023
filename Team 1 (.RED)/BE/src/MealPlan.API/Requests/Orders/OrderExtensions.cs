using MealPlan.Business.Orders.Commands;
using MealPlan.Business.Orders.Queries;

namespace MealPlan.API.Requests.Orders
{
    public static class OrderExtensions
    {
        public static ApproveOrderCommand ToCommand(this ApproveOrderRequest request)
        {
            return new ApproveOrderCommand
            {
                OrderID = request.OrderID
            };
        }

        public static AddOrderCommand ToCommand(this AddOrderRequest request, string userEmail)
        {
            return new AddOrderCommand
            {
                UserEmail = userEmail,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                MenuId = request.MenuId
            };
        }

        public static GetAllOrdersQuery ToQuery(this GetAllOrdersRequest request)
        {
            return new GetAllOrdersQuery
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                FilterByStatus = request.FilteringModel.FilterByStatus,
                SearchText = request.FilteringModel.SearchText,
                OrderByDescending = request.OrderingModel.OrderByDescending,
                OrderByColumns = request.OrderingModel.OrderByColumns
            };
        }

        public static DenyOrderCommand ToCommand(this DenyOrderRequest request)
        {
            return new DenyOrderCommand
            {
                OrderID = request.OrderID,
            };
        }
    }
}