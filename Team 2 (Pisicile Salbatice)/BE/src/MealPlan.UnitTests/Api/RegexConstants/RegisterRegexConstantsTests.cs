using AutoFixture;
using FluentAssertions;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Users;
using NUnit.Framework;
using System;

namespace MealPlan.UnitTests.Api.RegexConstants
{
    [TestFixture]
    public class RegisterRegexConstantsTests
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
        public void WhenFirstNameIsValidWithWhitespace_ShouldSucceed()
        {
            _request.FirstName = "Joe Bob";

            _validator.TestValidate(_request).ShouldNotHaveValidationErrorFor(r => r.FirstName);
        }

        [Test]
        public void WhenFirstNameIsValidWithApostrophe_ShouldSucceed()
        {
            _request.FirstName = "Joe o'Bob";

            _validator.TestValidate(_request).ShouldNotHaveValidationErrorFor(r => r.FirstName);
        }

        [Test]
        public void WhenFirstNameIsValidWithHyphen_ShouldSucceed()
        {
            _request.FirstName = "Joe-Bob";

            _validator.TestValidate(_request).ShouldNotHaveValidationErrorFor(r => r.FirstName);
        }

        [Test]
        public void WhenFirstNameIsValidWithSpecialCharactersNextToEachother_ShouldReturnError()
        {
            _request.FirstName = "Joe 'Bob";

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(r => r.FirstName);
        }

        [Test]
        public void WhenFirstNameHasWhitespacePrefix_ShouldReturnError()
        {
            _request.FirstName = "  Joe Bob";

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(r => r.FirstName);
        }

        [Test]
        public void WhenFirstNameHasWhitespaceSuffix_ShouldReturnError()
        {
            _request.FirstName = "Joe Bob  ";

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(r => r.FirstName);
        }

        [Test]
        public void WhenFirstNameHasMultipleWhitespaceInside_ShouldReturnError()
        {
            _request.FirstName = "Joe   Bob";

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(r => r.FirstName);
        }

        [Test]
        public void WhenFirstNameStartsWithSpecialCharacter_ShouldReturnError()
        {
            _request.FirstName = "'Joe Bob";

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(r => r.FirstName);
        }

        [Test]
        public void WhenLastNameIsValidWithWhitespace_ShouldSucceed()
        {
            _request.LastName = "Joe Bob";

            _validator.TestValidate(_request).ShouldNotHaveValidationErrorFor(r => r.LastName);
        }

        [Test]
        public void WhenLastNameIsValidWithApostrophe_ShouldSucceed()
        {
            _request.LastName = "Joe o'Bob";

            _validator.TestValidate(_request).ShouldNotHaveValidationErrorFor(r => r.LastName);
        }

        [Test]
        public void WhenLastNameIsValidWithHyphen_ShouldSucceed()
        {
            _request.LastName = "Joe-Bob";

            _validator.TestValidate(_request).ShouldNotHaveValidationErrorFor(r => r.LastName);
        }

        [Test]
        public void WhenLastNameIsValidWithSpecialCharactersNextToEachother_ShouldReturnError()
        {
            _request.LastName = "Joe 'Bob";

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(r => r.LastName);
        }

        [Test]
        public void WhenLastNameHasWhitespacePrefix_ShouldReturnError()
        {
            _request.LastName = "  Joe Bob";

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(r => r.LastName);
        }

        [Test]
        public void WhenLastNameHasWhitespaceSuffix_ShouldReturnError()
        {
            _request.LastName = "Joe Bob  ";

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(r => r.LastName);
        }

        [Test]
        public void WhenLastNameHasMultipleWhitespaceInside_ShouldReturnError()
        {
            _request.LastName = "Joe   Bob";

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(r => r.LastName);
        }

        [Test]
        public void WhenLastNameStartsWithSpecialCharacter_ShouldReturnError()
        {
            _request.LastName = "'Joe Bob";

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(r => r.LastName);
        }
    }
}
