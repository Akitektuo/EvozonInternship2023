using FluentAssertions;
using MealPlan.Business.Exceptions;
using MealPlan.Business.Users.Handlers;
using MealPlan.Business.Users.Models;
using MealPlan.Business.Users.Queries;
using MealPlan.Data;
using MealPlan.Data.Models.Users;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Business.Users.Handlers
{
    [TestFixture]
    public class LoginQueryHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private Mock<IConfiguration> _configuration;
        private LoginQueryHandler _handler;
        private LoginQuery _request;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _configuration = new Mock<IConfiguration>();

            _handler = new LoginQueryHandler(_context.Object, _configuration.Object);

            CreateRequest();
            SetupContext();
            SetupConfiguration();
        }

        [TearDown]
        public void Clean()
        {
            _context = null;
            _handler = null;
        }

        [Test]
        public async Task ShouldReturnCorrectUser()
        {
            var result = await _handler.Handle(_request, new CancellationToken());

            result.FirstName.Should().Be("Leo");
            result.LastName.Should().Be("Leo");
            result.Role.Should().Be(Role.User.ToString());
        }

        [Test]
        public async Task WhenUsingInvalidEmail_ShouldThrowError()
        {
            _request.Email = "leo@yahoooo.com";
            _request.Password = "leo123";

            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should()
                .ThrowAsync<CustomApplicationException>()
                .WithMessage(JsonSerializer.Serialize(
                    new CustomException
                    {
                        Message = "Bad credentials at login!",
                        Code = (int)ErrorCode.InvalidCredentials
                    }));
        }

        [Test]
        public async Task WhenUsingInvalidPassword_ShouldThrowError()
        {
            _request.Email = "leo@yahoo.com";
            _request.Password = "leo123456";

            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>();
        }

        [Test]
        public async Task WhenUsingDifferentCasePassword_ShouldThrowError()
        {
            _request.Password = "LeO123";

            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>();
        }

        [Test]
        public async Task WhenUsingSpacesInsidePassword_ShouldThrowError()
        {
            _request.Password = "leo12 3";

            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>();
        }

        private void SetupContext()
        {
            var users = new List<User>
            {
                new User
                {
                    Id = 1,
                    FirstName = "Leo",
                    LastName = "Leo",
                    Email = "leo@yahoo.com",
                    Password = "leo123",
                    RoleId = Role.User
                }
            };

            _context.Setup(c => c.Users).ReturnsDbSet(users);
        }

        private void SetupConfiguration()
        {
            _configuration.Setup(m => m["JWT:Issuer"]).Returns("testissuer");
            _configuration.Setup(m => m["JWT:Audience"]).Returns("testaudience");
            _configuration.Setup(m => m["JWT:Key"]).Returns(new string('k', 254));
        }

        private void CreateRequest()
        {
            _request = new LoginQuery { Email = "leo@yahoo.com", Password = "leo123" };
        }
    }
}