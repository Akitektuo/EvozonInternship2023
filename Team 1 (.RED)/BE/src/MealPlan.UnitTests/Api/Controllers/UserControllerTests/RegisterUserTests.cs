using FluentAssertions;
using MealPlan.API.Controllers;
using MealPlan.API.Requests.Users;
using MealPlan.Business.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Api.Controllers.UserControllerTests
{
    [TestFixture]
    public class RegisterUserTests
    {
        private UserController _controller;
        private Mock<IMediator> _mediator;
        private RegisterUserRequest _request;

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
        public async Task ShouldSendRegisterUser()
        {
            _mediator.Setup(m => m.Send(It.IsAny<RegisterUserCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(true));

            var result = await _controller.RegisterUser(_request);

            _mediator.Verify(m => m.Send(It.IsAny<RegisterUserCommand>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.RegisterUser(_request);
            
            result.Should().BeOfType<OkResult>();
        }

        private void CreateRequest()
        {
            _request = new RegisterUserRequest
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Email = "test@email.com",
                Password = "TestPassword",
                ConfirmPassword = "TestPassword"
            };
        }
    }
}