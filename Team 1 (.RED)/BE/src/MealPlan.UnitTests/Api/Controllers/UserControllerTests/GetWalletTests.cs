using FluentAssertions;
using MealPlan.API.Controllers;
using MealPlan.Business.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Api.Controllers.UserControllerTests
{
    [TestFixture]
    public class GetWalletTests
    {
        private UserController _controller;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Init()
        {
            _mediator = new Mock<IMediator>();

            _controller = new UserController(_mediator.Object);
        }

        [TearDown]
        public void Clean()
        {
            _controller = null;
        }

        [Test]
        public async Task ShouldSendGetWalletRequestQuery()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetWalletQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new double()));

            var auxClaim = new Claim(ClaimTypes.Email, "a@gmail.com");
            var claimsIdentity = new ClaimsIdentity(new List<Claim> { auxClaim }, "test_authentication_type");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = claimsPrincipal
                }
            };

            var result = await _controller.GetWallet();

            _mediator.Verify(m => m.Send(It.IsAny<GetWalletQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var auxClaim = new Claim(ClaimTypes.Email, "a@gmail.com");
            var claimsIdentity = new ClaimsIdentity(new List<Claim> { auxClaim }, "test_authentication_type");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = claimsPrincipal
                }
            };

            var result = await _controller.GetWallet();

            result.Result.Should().BeOfType<OkObjectResult>();
        }
    }
}