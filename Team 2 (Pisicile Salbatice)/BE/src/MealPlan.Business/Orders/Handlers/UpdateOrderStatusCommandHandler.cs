using MealPlan.Business.Exceptions;
using MealPlan.Business.Orders.Commands;
using MealPlan.Business.Services;
using MealPlan.Data.Models.Orders;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.Business.Orders.Handlers
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, bool>
    {
        private readonly IOrderStatusService _service;

        public UpdateOrderStatusCommandHandler(IOrderStatusService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var isChanged = request.StatusId switch
            {
                Status.Approved => await _service.ApproveOrder(request.OrderId),
                Status.Rejected => await _service.RejectOrder(request.OrderId),
                Status.WaitingForApproval => throw new CustomApplicationException(
                    ErrorCode.CannotResetStatus, $"Cannot update order id '{request.OrderId}' to '{request.StatusId.ToString()}'"),
                _ => throw new NotImplementedException()
            };

            return isChanged;
        }
    }
}
