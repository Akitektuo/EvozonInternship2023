using FluentAssertions;
using MealPlan.API.Controllers;
using MealPlan.API.Requests.Orders;
using MealPlan.API.Services.UserIdentity;
using MealPlan.Business.Orders.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Api.Controllers.OrderControllerTests
{
    public class AddOrderTests
    {
        private OrderController _controller;
        private Mock<IMediator> _mediator;
        private AddOrderRequest _request;
        private Mock<IUserIdentityService> _userIdentityService;

        [SetUp]
        public void Init()
        {
            _mediator = new Mock<IMediator>();

            _userIdentityService = new Mock<IUserIdentityService>();

            _userIdentityService.Setup( u => u.GetEmailClaim()).Returns(It.IsAny<string>());

            _controller = new OrderController(_mediator.Object, _userIdentityService.Object);

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
            _mediator.Setup(m => m.Send(It.IsAny<AddOrderCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new bool()));

            var result = await _controller.AddOrder(_request);

            _mediator.Verify(m => m.Send(It.IsAny<AddOrderCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.AddOrder(_request);

            result.Should().BeOfType<OkResult>();
        }

        private void CreateRequest()
        {
            _request = new AddOrderRequest
            {
                MenuId = 1,
                PhoneNumber = "0770 123 590",
                ShippingAddress = "21 Decembrie",
                StartDate = new DateTime().Date.AddDays(2),
                EndDate = new DateTime().Date.AddDays(7),
            };
        }
    }
}

