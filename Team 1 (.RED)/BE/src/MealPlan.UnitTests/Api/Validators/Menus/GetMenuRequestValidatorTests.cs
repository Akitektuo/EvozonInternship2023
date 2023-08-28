using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Menus;
using NUnit.Framework;

namespace MealPlan.UnitTests.Api.Validators.Menus
{
    [TestFixture]
    public class GetMenuRequestValidatorTests
    {
        private GetMenuRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _validator = new GetMenuRequestValidator();
            _fixture = new Fixture();
        }

        [Test]
        public void WhenIdMissing_ShouldReturnError()
        {
            var model = _fixture.Build<GetMenuRequest>()
                .Without(x => x.Id)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Test]
        public void WhenIdNegative_ShouldReturnError()
        {
            var model = _fixture.Build<GetMenuRequest>()
                .With(x => x.Id, -1)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.Id);
        }
    }
}