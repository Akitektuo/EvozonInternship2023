using FluentValidation.TestHelper;
using MealPlan.API.Requests.Users;
using NUnit.Framework;

namespace MealPlan.UnitTests.Api.RegexConstants
{
    [TestFixture]
    public class PasswordRegexConstantsTests
    {
        private RegisterRequestValidator _validator;
        private RegisterRequest _request;

        [SetUp]
        public void Init()
        {
            _validator = new RegisterRequestValidator();

            SetupRequest();
        }

        private void SetupRequest()
        {
            _request = new RegisterRequest
            {
                Email = "joe@test.com",
                FirstName = "Joe Bob",
                LastName = "Jhonas",
                Password = "12345",
                ConfirmedPassword = "12345"
            };
        }

        [Test]
        public void WhenPasswordDoesNotHaveAnyWhitespace_ShouldNotReturnError()
        {
            _request.Password = "12345";
            _request.ConfirmedPassword = _request.Password;

            _validator.TestValidate(_request).ShouldNotHaveValidationErrorFor(r => r.Password);
        }

        [Test]
        public void WhenPasswordHasWhitespacesPrefix_ShouldReturnError()
        {
            _request.Password = "   12345";
            _request.ConfirmedPassword = _request.Password;

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(r => r.Password);
        }

        [Test]
        public void WhenPasswordHasWhitespacesSuffix_ShouldReturnError()
        {
            _request.Password = "12345    ";
            _request.ConfirmedPassword = _request.Password;

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(r => r.Password);
        }

        [Test]
        public void WhenPasswordHasWhitespacesPrefixAndSuffix_ShouldReturnError()
        {
            _request.Password = "  12345    ";
            _request.ConfirmedPassword = _request.Password;

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(r => r.Password);
        }

        [Test]
        public void WhenPasswordHasWhitespacesInside_ShouldNotReturnError()
        {
            _request.Password = "123   45";
            _request.ConfirmedPassword = _request.Password;

            _validator.TestValidate(_request).ShouldNotHaveValidationErrorFor(r => r.Password);
        }
    }
}
