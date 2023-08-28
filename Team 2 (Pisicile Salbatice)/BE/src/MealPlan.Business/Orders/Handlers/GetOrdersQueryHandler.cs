using MealPlan.Business.Orders.Models;
using MealPlan.Business.Orders.Queries;
using MealPlan.Business.Shared;
using MealPlan.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.Business.Orders.Handlers
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, GetOrdersModel>
    {
        private readonly MealPlanContext _context;

        public GetOrdersQueryHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<GetOrdersModel> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var filteredOrders = await _context.Orders.ToOrderModel()
                .Filter(request.FiltrationModel)
                .Pagination(request.PageNumber, request.PageSize)
                .ToListAsync();

            return new GetOrdersModel
            {
                Orders = filteredOrders,
                TotalOrdersCount = filteredOrders.Count
            };
        }
    }
}
