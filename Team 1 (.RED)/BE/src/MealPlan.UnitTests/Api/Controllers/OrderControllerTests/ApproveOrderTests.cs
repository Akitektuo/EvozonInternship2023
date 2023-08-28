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
    public class ApproveOrderTests
    {
        private OrderController _controller;
        private Mock<IMediator> _mediator;
        private ApproveOrderRequest request;

        [SetUp]
        public void SetUp()
        {
            _mediator = new Mock<IMediator>();
            _controller = new OrderController(_mediator.Object);

            CreateRequest();
        }

        [TearDown]
        public void TearDown()
        {
            _controller = null;
        }

        [Test]
        public async Task ShouldSendApproveOrder()
        {
            _mediator.Setup(m => m.Send(It.IsAny<ApproveOrderCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            var result = await _controller.ApproveOrder(request, new CancellationToken());

            _mediator.Verify(m => m.Send(It.IsAny<ApproveOrderCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task ShouldSendOkResult()
        {
            _mediator.Setup(m => m.Send(It.IsAny<ApproveOrderCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            var result = await _controller.ApproveOrder(request, new CancellationToken());

            result.Should().BeOfType<OkResult>();
        }

        private void CreateRequest()
        {
            request = new ApproveOrderRequest
            {
                OrderID = 1
            };
        }
    }
}