using MealPlan.Business.Users.Handlers;
using MealPlan.Business.Users.Queries;
using MealPlan.Data;
using MealPlan.Data.Models.Users;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using FluentAssertions;
using System;
using MealPlan.Business.Exceptions;
using System.Text.Json;

namespace MealPlan.UnitTests.Business.Users.Handlers
{
    [TestFixture]
    public class GetWalletQueryHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private GetWalletQueryHandler _handler;
        private GetWalletQuery _successfulRequest;
        private GetWalletQuery _failRequest;

        [SetUp]
        public void Setup()
        {
            _context = new Mock<MealPlanContext>();

            _handler = new GetWalletQueryHandler(_context.Object);

            SetupContext();
            CreateRequest();
        }

        [TearDown]
        public void Teardown()
        {
            _context = null;
            _handler = null;
        }

        [Test]
        public async Task ShouldReturnWallet()
        {
            var result = await _handler.Handle(_successfulRequest, new CancellationToken());

            result.Should().Be(0);
        }

        [Test]
        public async Task ShouldReturnError()
        {
            Func<Task> action = async () => await _handler.Handle(_failRequest, new CancellationToken());

            await action.Should()
                 .ThrowAsync<CustomApplicationException>()
                 .WithMessage(JsonSerializer.Serialize(
                     new CustomException
                     {
                         Code = (int)ErrorCode.InvalidCredentials,
                         Message = "Invalid credentials"
                     }));
        }

        private void CreateRequest()
        {
            _successfulRequest = new GetWalletQuery { Email = "checkuser@gmail.com" };
            _failRequest = new GetWalletQuery { Email = "wrongmail@gmail.com" };
        }

        private void SetupContext()
        {
            var users = new List<User>
            {
                new User
                {
                    Email = "checkuser@gmail.com",
                    RoleId = Role.User
                }
            };

            _context.Setup(m => m.Users).ReturnsDbSet(users);
        }
    }
}