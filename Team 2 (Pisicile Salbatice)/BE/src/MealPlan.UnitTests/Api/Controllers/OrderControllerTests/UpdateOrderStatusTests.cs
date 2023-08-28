using MealPlan.API.Controllers;
using MealPlan.API.Requests.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Threading;
using MealPlan.Business.Orders.Commands;
using FluentAssertions;
using MealPlan.API.Services.UserIdentity;
using Microsoft.AspNetCore.Http;

namespace MealPlan.UnitTests.Api.Controllers.OrderControllerTests
{
    [TestFixture]
    public class UpdateOrderStatusTests
    {
        private OrderController _controller;
        private Mock<IMediator> _mediator;
        private UpdateOrderStatusRequest _request;
        private IUserIdentityService _userIdentityService;
        private IHttpContextAccessor _httpContext;

        [SetUp]
        public void Init()
        {
            _mediator = new Mock<IMediator>();
            _httpContext = new HttpContextAccessor();
            _userIdentityService = new UserIdentityService(_httpContext);

            _controller = new OrderController(_mediator.Object, _userIdentityService);

            CreateRequest();
        }

        [TearDown]
        public void Clean()
        {
            _controller = null;
        }

        [Test]
        public async Task ShouldSendOk()
        {
            _mediator.Setup(m => m.Send(It.IsAny<UpdateOrderStatusCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new bool()));

            var result = await _controller.UpdateOrderStatus(_request);

            _mediator.Verify(m => m.Send(It.IsAny<UpdateOrderStatusCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.UpdateOrderStatus(_request);

            result.Should().BeOfType<OkResult>();
        }

        private void CreateRequest()
        {
            _request = new UpdateOrderStatusRequest
            { 
                StatusId = Data.Models.Orders.Status.Approved,
                OrderId = 1
            };
        }
    }
}
