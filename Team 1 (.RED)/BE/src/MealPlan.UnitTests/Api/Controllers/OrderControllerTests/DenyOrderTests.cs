using FluentAssertions;
using MealPlan.API.Controllers;
using MealPlan.API.Requests.Orders;
using MealPlan.Business.Orders.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Api.Controllers.OrderControllerTests
{
    [TestFixture]
    public class DenyOrderTests
    {
        private OrderController _controller;
        private DenyOrderRequest _request;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _mediator = new Mock<IMediator>();
            _controller = new OrderController(_mediator.Object);

            _request = new DenyOrderRequest
            {
                OrderID = 1
            };
        }

        [TearDown]
        public void TearDown()
        {
            _mediator = null;
            _controller = null;
        }

        [Test]
        public async Task ShouldSendDenyOrder()
        {
            _mediator.Setup(m => m.Send(It.IsAny<DenyOrderCommand>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));

            var result = await _controller.DenyOrder(_request, new CancellationToken());

            _mediator.Verify(m => m.Send(It.IsAny<DenyOrderCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task ShouldReturnOkResult()
        {
            _mediator.Setup(m => m.Send(It.IsAny<DenyOrderCommand>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));

            var result = await _controller.DenyOrder(_request, new CancellationToken());

            result.Should().BeOfType<OkResult>();
        }
    }
}