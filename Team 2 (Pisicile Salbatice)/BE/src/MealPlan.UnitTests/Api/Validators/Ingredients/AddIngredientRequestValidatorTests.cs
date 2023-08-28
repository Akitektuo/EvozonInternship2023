using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Ingredients;
using NUnit.Framework;

namespace MealPlan.UnitTests.Api.Validators.Ingredients
{
    [TestFixture]
    public class AddIngredientRequestValidatorTests
    {
        private AddIngredientRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _validator = new AddIngredientRequestValidator();
            _fixture = new Fixture();
        }

        [Test]
        public void WhenGoodData_ShouldNotReturnError()
        {
            var model = _fixture.Build<AddIngredientRequest>()
                .With(x => x.IngredientName, "ingredient")
                .Create();

            _validator.TestValidate(model).ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void WhenEmptyIngredientName_ShouldReturnError()
        {
            var model = _fixture.Build<AddIngredientRequest>()
                .Without(x => x.IngredientName)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.IngredientName);
        }
    }
}
