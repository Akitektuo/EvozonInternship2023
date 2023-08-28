using FluentAssertions;
using MealPlan.API.Controllers;
using MealPlan.API.Requests.Users;
using MealPlan.Business.Users.Models;
using MealPlan.Business.Users.Queries;
using MealPlan.Data.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Api.Controllers.UserControllerTests
{
    [TestFixture]
    public class LoginTests
    {
        private UserController _controller;
        private Mock<IMediator> _mediator;
        private LoginRequest _request;

        [SetUp]
        public void Init()
        {
            _mediator = new Mock<IMediator>();

            _controller = new UserController(_mediator.Object);

            CreateRequest();
        }

        [TearDown]
        public void Clean()
        {
            _controller = null;
        }

        [Test]
        public async Task ShouldSendLoginQuery()
        {
            _mediator.Setup(m => m.Send(It.IsAny<LoginQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new UserDetails
                {
                    FirstName = "Paula",
                    LastName = "Costea",
                    Token = "paula@yahoo.com",
                    Role = Role.User.ToString()
                }));

            var result = await _controller.Login(_request);

            _mediator.Verify(m => m.Send(It.IsAny<LoginQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.Login(_request);

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        private void CreateRequest()
        {
            _request = new LoginRequest { Email = "paula@yahoo.com", Password = "paula123" };
        }
    }
}