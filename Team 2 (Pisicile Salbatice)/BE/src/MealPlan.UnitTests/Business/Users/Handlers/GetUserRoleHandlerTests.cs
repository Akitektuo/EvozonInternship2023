using MealPlan.API.Services.Authorization;
using MealPlan.Data.Models.Users;
using MealPlan.Data;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using MealPlan.Business.Users.Handlers;
using System.Threading;
using MealPlan.Business.Users.Queries;
using FluentAssertions;
using System.Threading.Tasks;
using Moq.EntityFrameworkCore;

namespace MealPlan.UnitTests.Business.Users.Handlers
{
    [TestFixture]
    public class GetUserRoleHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private GetUserRoleHandler _handler;
        private GetUserRoleQuery _request;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new GetUserRoleHandler(_context.Object);

            CreateRequest();
            SetupContext();
        }

        [Test]
        public async Task WhenUserIsAdmin_ShouldReturnAdminRole()
        {
            var result = await _handler.Handle(_request, new CancellationToken());

            result.Should().Be(Role.Admin);
        }

        [Test]
        public async Task WhenUserIsDefaultUser_ShouldReturnUserRole()
        {
            _request.Email = "ana@test.com";
            var result = await _handler.Handle(_request, new CancellationToken());

            result.Should().Be(Role.User);
        }

        private void SetupContext()
        {
            var users = new List<User>
            {
                new User { Id = 1, Email = "joe@test.com", Password = "joejoe", FirstName = "Joe", LastName = "Johnathan", RoleId = Role.Admin },
                new User { Id = 2, Email = "ana@test.com", Password = "anaana", FirstName = "Ana", LastName = "Johnathan", RoleId = Role.User }
            };

            _context.Setup(c => c.Users).ReturnsDbSet(users);
        }

        private void CreateRequest()
        {
            _request = new GetUserRoleQuery
            {
                Email = "joe@test.com"
            };
        }
    }
}
