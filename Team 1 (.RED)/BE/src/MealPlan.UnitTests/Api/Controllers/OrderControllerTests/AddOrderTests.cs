using AutoFixture;
using FluentAssertions;
using MealPlan.API.Controllers;
using MealPlan.API.Requests.Orders;
using MealPlan.Business.Orders.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Api.Controllers.OrderControllerTests
{
    [TestFixture]
    public class AddOrderTests
    {
        private OrderController _controller;
        private Mock<IMediator> _mediator;
        private AddOrderRequest _request; 

        [SetUp]
        public void Init()
        {
            _mediator = new Mock<IMediator>();

            _controller = new OrderController(_mediator.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal
                        (
                            new List<ClaimsIdentity>
                            {
                                new ClaimsIdentity
                                (
                                    new List<Claim>
                                    {
                                        new Claim(ClaimTypes.Email, "test@gmail.com")
                                    }
                                )
                            }
                        )
                    }
                }
            };

            CreateRequest();
        }

        [TearDown]
        public void Clean()
        {
            _controller = null;
        }

        [Test]
        public async Task ShouldSendAddMeal()
        {
            _mediator.Setup(m => m.Send(It.IsAny<AddOrderCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            var result = await _controller.AddOrder(_request);

            _mediator.Verify(m => m.Send(It.IsAny<AddOrderCommand>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.AddOrder(_request);

            result.Should().BeOfType<OkResult>();
        }

        private void CreateRequest()
        {
            _request = new AddOrderRequest()
            {
                Address = "Address",
                PhoneNumber = "12345",
                StartDate = new DateTime(2023, 8, 17),
                EndDate = new DateTime(2023, 8, 18),
                MenuId = 1
            };
        }
    }
}