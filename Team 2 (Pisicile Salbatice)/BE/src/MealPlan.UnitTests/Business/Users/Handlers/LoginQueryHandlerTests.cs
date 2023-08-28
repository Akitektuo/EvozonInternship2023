using MealPlan.Data;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MealPlan.Business.Users.Handlers;
using MealPlan.Business.Users.Queries;
using MealPlan.Data.Models.Users;
using Moq.EntityFrameworkCore;
using FluentAssertions;
using System;
using MealPlan.Business.Users.Models;
using NUnit.Framework.Internal;
using AutoFixture;
using MealPlan.Business.Exceptions;

namespace MealPlan.UnitTests.Business.Users.Handlers
{
    [TestFixture]
    public class LoginQueryHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private LoginQueryHandler _handler;
        private LoginQuery _request;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new LoginQueryHandler(_context.Object);
            _fixture = new Fixture();

            CreateRequest();
            SetupContext();
        }

        [TearDown]
        public void Clean()
        {
            _context = null;
            _handler = null;
        }

        [Test]
        public async Task ShouldReturnCorrectUserId()
        {
            var result = await _handler.Handle(_request, new CancellationToken());

            result.Id.Should().Be(1);
        }

        [Test]
        public async Task WhenUsingInvalidEmail_ShouldThrowExceptions()
        {
            _request.Email = "joewrongemail@test.com";

            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>().Where(e => e.ErrorCode == ErrorCode.BadCredentials);
        }

        [Test]
        public async Task WhenUsingInvalidPassword_ShouldThrowExceptions()
        {
            _request.Password = "joewrongpassword";

            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>().Where(e => e.ErrorCode == ErrorCode.BadCredentials);
        }

        [Test]
        public async Task WhenUsingValidEmailAndPassword_ShouldReturnValidResponse()
        {
            var loginModel = _fixture.Build<LoginQuery>()
                .With(x => x.Email, "joe@test.com")
                .With(x => x.Password, "joejoe")
                .Create();

            var result = await _handler.Handle(loginModel, new CancellationToken());

            result.Should().BeEquivalentTo(new LoginModel { Id = 1, Email = "joe@test.com", FirstName = "Joe", LastName = "Johnathan", RoleId = Role.User, Balance = 200 });
        }

        private void SetupContext()
        {
            var users = new List<User>
            {
                new User { Id = 1, Email = "joe@test.com", Password = "joejoe", FirstName = "Joe", LastName = "Johnathan", RoleId = Role.User, Balance = 200 },
                new User { Id = 2, Email = "ana@test.com", Password = "anaana", FirstName = "Ana", LastName = "Johnathan", RoleId = Role.User, Balance = 100 }
        };

            _context.Setup(c => c.Users).ReturnsDbSet(users);
        }

        private void CreateRequest()
        {
            _request = new LoginQuery { Email = "joe@test.com", Password = "joejoe" };
        }
    }
}
