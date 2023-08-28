using MealPlan.Business.Orders.Models;
using MealPlan.Business.Orders.Queries;
using MealPlan.Business.Utils;
using MealPlan.Data;
using MealPlan.Data.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.Business.Orders.Handlers
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, PaginationModel<OrderDetails>>
    {
        private readonly MealPlanContext _context;

        public GetAllOrdersQueryHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<PaginationModel<OrderDetails>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders
                .DataProcessing(request.FilterByStatus, request.SearchText, request.OrderByColumns, request.OrderByDescending)
                .ToOrderDetails()
                .ToPage(request.PageNumber, request.PageSize)
                .ToListAsync();

            var totalNoOfItems = await _context.Orders
                .DataProcessing(request.FilterByStatus, request.SearchText, request.OrderByColumns, request.OrderByDescending)
                .CountAsync();

            return new PaginationModel<OrderDetails> { Items = orders, TotalRecords = totalNoOfItems };
        }
    }
}