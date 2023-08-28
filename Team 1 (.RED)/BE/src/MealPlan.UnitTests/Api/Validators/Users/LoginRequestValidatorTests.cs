using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Users;
using MealPlan.UnitTests.Utils;
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
        public void WhenEmailMissing_ShouldReturnError()
        {
            var model = _fixture.Build<LoginRequest>()
                .Without(x => x.Email)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Email);
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
        public void WhenEmailDoesntHaveEmailFormat_ShouldReturnError()
        {
            var model = _fixture.Build<LoginRequest>()
                .With(x => x.Email, "paula12345")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Email);
        }

        [Test]
        public void WhenEmailTooShort_ShouldReturnError()
        {
            var model = _fixture.Build<LoginRequest>()
                .With(x => x.Email, "a@a.ro")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Email);
        }

        [Test]
        public void WhenEmailTooLong_ShouldReturnError()
        {
            var model = _fixture.Build<LoginRequest>()
                .With(x => x.Email, StringUtils.RandomString(55) + "@yahoo.com")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Email);
        }

        [Test]
        public void WhenPassswordTooLong_ShouldReturnError()
        {
            var model = _fixture.Build<LoginRequest>()
                .With(x => x.Password, StringUtils.RandomString(55) + "@yahoo.com")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Password);
        }

        [Test]
        public void WhenPassswordTooShort_ShouldReturnError()
        {
            var model = _fixture.Build<LoginRequest>()
                .With(x => x.Password, "abc")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Password);
        }
    }
}