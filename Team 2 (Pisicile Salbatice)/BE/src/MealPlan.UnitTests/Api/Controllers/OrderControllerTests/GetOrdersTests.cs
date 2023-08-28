using FluentAssertions;
using MealPlan.API.Controllers;
using MealPlan.API.Requests.Orders;
using MealPlan.API.Services.UserIdentity;
using MealPlan.Business.Orders.Models;
using MealPlan.Business.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Api.Controllers.OrderControllerTests
{
    [TestFixture]
    public class GetOrdersTests
    {
        private OrderController _controller;
        private Mock<IMediator> _mediator;
        private GetOrdersRequest _request;

        [SetUp]
        public void Init()
        {
            _mediator = new Mock<IMediator>();

            _controller = new OrderController(_mediator.Object, new Mock<IUserIdentityService>().Object);

            _request = new GetOrdersRequest();
        }

        [TearDown]
        public void Clean()
        {
            _controller = null;
        }

        [Test]
        public async Task ShouldSendGetOrdersQuery()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetOrdersQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new GetOrdersModel()));

            var result = await _controller.GetOrders(_request);

            _mediator.Verify(m => m.Send(It.IsAny<GetOrdersQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.GetOrders(_request);

            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
