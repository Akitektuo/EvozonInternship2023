using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Ingredients;
using MealPlan.UnitTests.Utils;
using NUnit.Framework;

namespace MealPlan.UnitTests.Api.Validators.Ingredients
{
    [TestFixture]
    public class AddIngredientValidatorTests
    {
        private AddIngredientRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _fixture = new Fixture();
            _validator = new AddIngredientRequestValidator();
        }

        [Test]
        public void WhenNameMissing_ShouldReturnError()
        {
            var model = _fixture.Build<AddIngredientRequest>()
                .Without(x => x.Name)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Test]
        public void WhenNameLongerThanMax_ShouldReturnError()
        {
            var model = _fixture.Build<AddIngredientRequest>()
                .With(x => x.Name, StringUtils.RandomString(51))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Test]
        public void WhenNameShorterThanMin_ShouldReturnError()
        {
            var model = _fixture.Build<AddIngredientRequest>()
                .With(x => x.Name, StringUtils.RandomString(1))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }
    }
}