using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Users;
using NUnit.Framework;

namespace MealPlan.UnitTests.Api.Validators.Users
{
    [TestFixture]
    public class RegisterRequestValidatorTests
    {
        private RegisterRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _validator = new RegisterRequestValidator();
            _fixture = new Fixture();
        }

        [Test]
        public void WhenGoodCredentials_ShouldNotReturnError()
        {
            var model = _fixture.Build<RegisterRequest>()
                .With(x => x.FirstName, "firstName")
                .With(x => x.LastName, "lastName")
                .With(x => x.Email, "goodformat@test.com")
                .With(x => x.Password, "password")
                .With(x => x.ConfirmedPassword, "password")
                .Create();

            _validator.TestValidate(model).ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void WhenFirstNameMissing_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterRequest>()
                .Without(x => x.FirstName)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.FirstName);
        }

        [Test]
        public void WhenFirstNameTooLong_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterRequest>()
                .With(x => x.FirstName, "firstNamefirstNamefirstNamefirstNamefirstNamefirstName")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.FirstName);
        }

        [Test]
        public void WhenLastNameMissing_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterRequest>()
                .Without(x => x.LastName)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.LastName);
        }

        [Test]
        public void WhenLastNameTooLong_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterRequest>()
                .With(x => x.LastName, "lastNamelastNamelastNamelastNamelastNamelastNamelastName")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.LastName);
        }

        [Test]
        public void WhenEmailMissing_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterRequest>()
                .Without(x => x.Email)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Email);
        }

        [Test]
        public void WhenEmailTooShort_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterRequest>()
                .With(x => x.Email, "a@a.a")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Email);
        }

        [Test]
        public void WhenEmailTooLong_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterRequest>()
                .With(x => x.Email, "emailtoolongemailtoolongemailtoolongemailtoolongemailtoolongemailtoolongemailtoolong@emailtoolong.emailtoolong")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Email);
        }

        [Test]
        public void WhenEmailBadFormat_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterRequest>()
                .With(x => x.Email, "badformat.com")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Email);
        }

        [Test]
        public void WhenPasswordMissing_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterRequest>()
                .Without(x => x.Password)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Password);
        }

        [Test]
        public void WhenPasswordShorterThan5Chars_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterRequest>()
                .With(x => x.Password, "shrt")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Password);
        }

        [Test]
        public void WhenPasswordLongerThan20Chars_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterRequest>()
                .With(x => x.Password, "passwordtoolongpasswordtoolongpasswordtoolong")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Password);
        }

        [Test]
        public void WhenConfirmedPasswordMissing_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterRequest>()
                .Without(x => x.ConfirmedPassword)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.ConfirmedPassword);
        }

        [Test]
        public void WhenConfirmedPasswordShorterThan5Chars_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterRequest>()
                .With(x => x.ConfirmedPassword, "shrt")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.ConfirmedPassword);
        }

        [Test]
        public void WhenConfirmedPasswordLongerThan20Chars_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterRequest>()
                .With(x => x.ConfirmedPassword, "passwordtoolongpasswordtoolongpasswordtoolong")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.ConfirmedPassword);
        }

        [Test]
        public void WhenConfirmedPasswordNotEqualToPassword_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterRequest>()
                .With(x => x.Password, "password")
                .With(x => x.ConfirmedPassword, "confirmedPassword")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.ConfirmedPassword);
        }
    }
}