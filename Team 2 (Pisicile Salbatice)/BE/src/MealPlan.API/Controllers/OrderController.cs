using MealPlan.API.Authorization;
using MealPlan.API.Requests.Orders;
using MealPlan.API.Services.UserIdentity;
using MealPlan.Data.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MealPlan.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IUserIdentityService _userIdentityService;

        public OrderController(IMediator mediator, IUserIdentityService userIdentityService)
        {
            _mediator = mediator;
            _userIdentityService = userIdentityService; 
        }

        [AuthorizeRoles(Role.Admin)]
        [HttpPost("update-order")]
        public async Task<ActionResult> UpdateOrderStatus([FromBody] UpdateOrderStatusRequest request)
        {
            await _mediator.Send(request.ToCommand());

            return Ok();
        }

        [AuthorizeRoles(Role.Admin)]
        [HttpPost("get-orders")]
        public async Task<ActionResult> GetOrders([FromBody] GetOrdersRequest request)
        { 
            var result = await _mediator.Send(request.ToQuery());

            return Ok(result);
        }
	
        [AuthorizeRoles(Role.User)]
        [HttpPost("add-order")]
        public async Task<ActionResult> AddOrder([FromBody] AddOrderRequest request)
        {
            var email = _userIdentityService.GetEmailClaim();
            request.Email = email;

            await _mediator.Send(request.ToCommand());

            return Ok();
        }
    }
}
