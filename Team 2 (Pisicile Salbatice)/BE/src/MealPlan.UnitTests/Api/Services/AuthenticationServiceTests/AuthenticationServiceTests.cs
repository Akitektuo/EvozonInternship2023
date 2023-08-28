using FluentAssertions;
using MealPlan.API.Services.Authentication;
using MealPlan.Business.Users.Models;
using MealPlan.Data.Models.Users;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Collections.Generic;

namespace MealPlan.UnitTests.Api.Services.AuthenticationServiceTests
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private AuthenticationService _authenticationService;
        private LoginModel _loginModel;
        private Microsoft.Extensions.Configuration.IConfiguration _configuration;

        [SetUp]
        public void Init()
        {
            var myConfigurationData = new Dictionary<string, string>
            {
                { "JWT:ValidAudience", "JWTAuthenticationHIGHsecuredPasswordDLVp6OH7Xxof"},
                {"JWT:ValidIssuer", "http://localhost:44315"},
                {"JWT:Secret", "http://localhost:44315"}
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfigurationData)
                .Build();

            _authenticationService = new AuthenticationService(_configuration);

            CreateLoginModel();
        }

        [TearDown]
        public void Clean()
        {
            _loginModel = null;
        }

        [Test]
        public void GivenLoginModel_ShouldAddTokenParameter()
        {
            _loginModel = _authenticationService.GenerateJwt(_loginModel);
            _loginModel.Token.Should().NotBeNull();
        }

        public void CreateLoginModel()
        {
            _loginModel = new LoginModel
            {
                Id = 1,
                Email = "test@email.com",
                FirstName = "John",
                LastName = "Snow",
                RoleId = Role.User
            };
        }
    }
}
