using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Users;
using NUnit.Framework;

namespace MealPlan.UnitTests.Api.Validators.Users
{
    [TestFixture]
    public class LoginRequestValidatorTests
    {
        private LoginRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _validator = new LoginRequestValidator();
            _fixture = new Fixture();
        }

        [Test]
        public void WhenGoodCredentials_ShouldNotReturnError()
        {
            var model = _fixture.Build<LoginRequest>()
                .With(x => x.Email, "goodformat@test.com")
                .With(x => x.Password, "password")
                .Create();

            _validator.TestValidate(model).ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void WhenEmailMissing_ShouldReturnError()
        {
            var model = _fixture.Build<LoginRequest>()
                .Without(x => x.Email)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Email);
        }

        [Test]
        public void WhenEmailTooShort_ShouldReturnError()
        {
            var model = _fixture.Build<LoginRequest>()
                .With(x => x.Email, "a@a.a")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Email);
        }

        [Test]
        public void WhenEmailTooLong_ShouldReturnError()
        {
            var model = _fixture.Build<LoginRequest>()
                .With(x => x.Email, "emailtoolongemailtoolongemailtoolongemailtoolongemailtoolongemailtoolongemailtoolong@emailtoolong.emailtoolong")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Email);
        }

        [Test]
        public void WhenEmailBadFormat_ShouldReturnError()
        {
            var model = _fixture.Build<LoginRequest>()
                .With(x => x.Email, "badformat.com")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Email);
        }

        [Test]
        public void WhenEmailGoodFormat_ShouldNotReturnError()
        {
            var model = _fixture.Build<LoginRequest>()
                .With(x => x.Email, "goodformat@afdas.com")
                .Create();

            _validator.TestValidate(model).ShouldNotHaveValidationErrorFor(model => model.Email);
        }

        [Test]
        public void WhenPasswordMissing_ShouldReturnError()
        {
            var model = _fixture.Build<LoginRequest>()
                .Without(x => x.Password)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Password);
        }

        [Test]
        public void WhenPasswordLongerThan20Chars_ShouldReturnError()
        {
            var model = _fixture.Build<LoginRequest>()
                .With(q => q.Password, "passwordtoolongpasswordtoolongpasswordtoolong")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Password);
        }

        [Test]
        public void WhenPasswordShorterThan5Chars_ShouldReturnError()
        {
            var model = _fixture.Build<LoginRequest>()
                .With(q => q.Password, "shrt")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Password);
        }
    }
}
