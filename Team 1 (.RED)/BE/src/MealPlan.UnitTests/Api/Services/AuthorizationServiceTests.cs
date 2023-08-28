using FluentAssertions;
using MealPlan.API.Services;
using MealPlan.Business.Users.Queries;
using MealPlan.Data.Models.Users;
using MediatR;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Api.Services
{
    [TestFixture]
    public class AuthorizationServiceTests
    {
        private Mock<IMediator> _mediator;
        private IAuthorizationService _authorizationService;
        private string _email;

        [SetUp]
        public void Init()
        {
            _mediator = new Mock<IMediator>();

            _authorizationService = new AuthorizationService(_mediator.Object);

            _email = "test@email.com";
        }

        [TearDown]
        public void TearDown()
        {
            _authorizationService = null;
        }

        [Test]
        public async Task ShouldSendGetRoleQuery()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetRoleQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Role.User));

            var result = await _authorizationService.IsAuthorized(_email, new List<Role> { });

            _mediator.Verify(m => m.Send(It.IsAny<GetRoleQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenAllowedRolesEmpty_ReturnsFalse()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetRoleQuery>(), It.IsAny<CancellationToken>()))
               .Returns(Task.FromResult(Role.User));

            var result = await _authorizationService.IsAuthorized(_email, new List<Role> { });

            result.Should().BeFalse();
        }

        [Test]
        public async Task WhenAllowedRolesHasUserRole_ReturnsTrue()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetRoleQuery>(), It.IsAny<CancellationToken>()))
               .Returns(Task.FromResult(Role.User));

            var result = await _authorizationService.IsAuthorized(_email, new List<Role> {Role.User});

            result.Should().BeTrue();
        }

        [Test]
        public async Task WhenAllowedRolesDoesntHaveRole_ReturnsFalse()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetRoleQuery>(), It.IsAny<CancellationToken>()))
               .Returns(Task.FromResult(Role.User));

            var result = await _authorizationService.IsAuthorized(_email, new List<Role> {Role.Admin });

            result.Should().BeFalse();
        }
    }
}