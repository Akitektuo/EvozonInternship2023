using MealPlan.API.Middleware;
using MealPlan.API.Requests.Menus;
using MealPlan.API.Requests.Orders;
using MealPlan.Business.Orders.Models;
using MealPlan.Business.Utils;
using MealPlan.Data.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [RolesAuth(Role.Admin, Role.User)]
        [HttpPost("add-order")]
        public async Task<ActionResult> AddOrder([FromBody] AddOrderRequest request)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email).Value;

            var result = await _mediator.Send(request.ToCommand(userEmail));

            return Ok();
        }

        [RolesAuth(Role.Admin)]
        [HttpPost("approve-order/{OrderID}")]
        public async Task<ActionResult> ApproveOrder([FromRoute] ApproveOrderRequest request, CancellationToken token)
        {
            var result = await _mediator.Send(request.ToCommand());

            return Ok();
        }

        [RolesAuth(Role.Admin)]
        [HttpPost("get-all-orders")]
        public async Task<ActionResult<PaginationModel<OrderDetails>>> GetAllOrders([FromBody] GetAllOrdersRequest request)
        {
            var result = await _mediator.Send(request.ToQuery());

            return Ok(result);
        }

        [RolesAuth(Role.Admin)]
        [HttpPost("deny-order/{OrderID}")]
        public async Task<ActionResult> DenyOrder([FromRoute] DenyOrderRequest request, CancellationToken token)
        {
            var result = await _mediator.Send(request.ToCommand());

            return Ok();
        }
    }
}