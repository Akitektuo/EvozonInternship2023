using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Users;
using NUnit.Framework;

namespace MealPlan.UnitTests.Api.Validators.Users
{
    [TestFixture]
    public class RegisterUserRequestValidatorTests
    {
        private RegisterUserRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _validator = new RegisterUserRequestValidator(); 
            _fixture = new Fixture();
        }

        [Test]
        public void WhenFirstNameMissing_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterUserRequest>()
                .Without(x => x.FirstName)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.FirstName);
        }

        [Test]
        public void WhenFirstNameLongerThanMax_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterUserRequest>()
                .With(x => x.FirstName, new string('A', 51))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.FirstName);
        }

        [Test]
        public void WhenLastNameMissing_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterUserRequest>()
                .Without(x => x.LastName)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.LastName);
        }

        [Test]
        public void WhenLastNameLongerThanMax_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterUserRequest>()
                .With(x => x.LastName, new string('A', 51))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.LastName);
        }

        [Test]
        public void WhenPasswordMissing_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterUserRequest>()
                .Without(x => x.Password)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Password);
        }

        [Test]
        public void WhenPasswordLongerThanMax_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterUserRequest>()
                .With(x => x.Password, new string('A', 21))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Password);
        }

        [Test]
        public void WhenPasswordShorterThanMin_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterUserRequest>()
                .With(x => x.Password, new string('A', 4))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Password);
        }

        [Test]
        public void WhenPasswordStartsWithWhitespaces_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterUserRequest>()
                .With(x => x.Password, "   testPassword")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Password);
        }

        [Test]
        public void WhenPasswordHasWhitespaces_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterUserRequest>()
                .With(x => x.Password, "test Password")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Password);
        }

        [Test]
        public void WhenPasswordEndsWithWhitespaces_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterUserRequest>()
                .With(x => x.Password, "testPassword  ")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Password);
        }

        [Test]
        public void WhenEmailMissing_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterUserRequest>()
                .Without(x => x.Email)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Email);
        }

        [Test]
        public void WhenEmailLongerThanMax_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterUserRequest>()
                .With(x => x.Email, new string('A', 50) + "@gmail.com")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Email);
        }

        [Test]
        public void WhenEmailShorterThanMin_ShouldReturnError()
        {
            string email = "a@a.ro";
            var model = _fixture.Build<RegisterUserRequest>()
                .With(x => x.Email, email)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Email);
        }

        [Test]
        public void WhenEmailDoesntHaveEmailFormat_ShouldReturnError()
        {
            string email = "aaaaaaaa";
            var model = _fixture.Build<RegisterUserRequest>()
                .With(x => x.Email, email)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Email);
        }

        [Test]
        public void WhenNameContainsDigits_ShouldReturnError()
        {
            string firstName = "FirstName12";
            string lastName = "1LastName";

            var model = _fixture.Build<RegisterUserRequest>()
                .With(x => x.FirstName, firstName)
                .With(x => x.LastName, lastName)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.FirstName);
            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.LastName);
        }

        [Test]
        public void WhenConfirmPasswordDoesntMatchPassword_ShouldReturnError()
        {
            var model = _fixture.Build<RegisterUserRequest>()
                .With(x => x.Password, new string('A', 6))
                .With(x => x.ConfirmPassword, new string('B', 7))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model =>  model.ConfirmPassword);
        }
    }
}