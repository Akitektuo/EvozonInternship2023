using FluentAssertions;
using MealPlan.Business.Exceptions;
using MealPlan.Business.Users.Commands;
using MealPlan.Business.Users.Handlers;
using MealPlan.Data;
using MealPlan.Data.Models.Users;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Business.Users.Handlers
{
    [TestFixture]
    public class RegisterCommandHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private RegisterCommandHandler _handler;
        private RegisterCommand _request;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new RegisterCommandHandler(_context.Object);

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
        public async Task WhenUsingEmailAlreadyRegistred_ShouldFailRegistration()
        {
            _request.Email = "ana@test.com";

            Func<Task> action = () => _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowExactlyAsync<CustomApplicationException>().Where(e => e.ErrorCode == ErrorCode.UserAlreadyExists);
        }

        [Test]
        public async Task WhenUsingValidEmail_ShouldSucceedRegistration()
        {
            _request.Email = "joe@test.com";

            var result = await _handler.Handle(_request, new CancellationToken());

            result.Should().BeTrue();
        }

        private void SetupContext()
        {
            var users = new List<User>
            {
                new User { Id = 1, Email = "ana@test.com", Password = "anaana", FirstName = "Ana", LastName = "Johnathan", RoleId = Role.User}
            };

            _context.Setup(c => c.Users).ReturnsDbSet(users);
        }

        private void CreateRequest()
        {
            _request = new RegisterCommand() { FirstName = "Joe", LastName = "Johnathan", Email = "joe@test.com", Password = "joejoe", ConfirmedPassword = "joejoe" };
        }
    }
}
