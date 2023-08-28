using FluentAssertions;
using MealPlan.Business.Users.Handlers;
using MealPlan.Business.Users.Queries;
using MealPlan.Data;
using MealPlan.Data.Models.Users;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Business.Users.Handlers
{
    [TestFixture]
    public class GetRoleQueryHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private GetRoleQueryHandler _handler;
        private GetRoleQuery _userRequest;
        private GetRoleQuery _adminRequest;
        private GetRoleQuery _badRequest;

        [SetUp]
        public void Setup()
        {
            _context = new Mock<MealPlanContext>();

            _handler = new GetRoleQueryHandler(_context.Object);

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
        public async Task ShouldReturnUserRole()
        {
            var result = await _handler.Handle(_userRequest, new CancellationToken());

            result.Should().BeOneOf(Role.User);
        }

        [Test]
        public async Task ShouldReturnAdminRole()
        {
            var result = await _handler.Handle(_adminRequest, new CancellationToken());

            result.Should().BeOneOf(Role.Admin);
        }

        [Test]
        public async Task ShouldReturnDefaultRole()
        {
            var result = await _handler.Handle(_badRequest, new CancellationToken());

            result.Should().Be(0);
        }

        private void CreateRequest()
        {
            _userRequest = new GetRoleQuery { Email = "checkuser@gmail.com" };
            _adminRequest = new GetRoleQuery { Email = "checkadmin@gmail.com" };
            _badRequest = new GetRoleQuery { Email = "anotheronebitesthedust" };
        }

        private void SetupContext()
        {
            var users = new List<User>
            {
                new User
                {
                    Email = "checkuser@gmail.com",
                    RoleId = Role.User
                },
                new User
                {
                    Email = "checkadmin@gmail.com",
                    RoleId = Role.Admin
                }
            };

            _context.Setup(m => m.Users).ReturnsDbSet(users);
        }
    }
}