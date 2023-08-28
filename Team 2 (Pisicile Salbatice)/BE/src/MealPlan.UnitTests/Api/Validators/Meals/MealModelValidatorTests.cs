using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Menus;
using MealPlan.Data.Models.Meals;
using NUnit.Framework;
using MealPlan.UnitTests.Shared;

namespace MealPlan.UnitTests.Api.Validators.Meals
{
    [TestFixture]
    public class MealModelValidatorTests
    {
        private MealReceivedValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _validator = new MealReceivedValidator();
            _fixture = new Fixture();
        }

        [Test]
        public void WhenGoodData_ShouldNotReturnError()
        {
            var model = _fixture.Build<MealReceived>()
                .With(x => x.Price, 10)
                .With(x => x.MealTypeId, MealType.Breakfast)
                .With(x => x.Name, "test")
                .Create();

            _validator.TestValidate(model).ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void WhenNameIsMissing_ShouldReturnError()
        {
            var model = _fixture.Build<MealReceived>()
                .Without(x => x.Name)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Test]
        public void WhenDescriptionIsMissing_ShouldReturnError()
        {
            var model = _fixture.Build<MealReceived>()
                .Without(x => x.Description)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Description);
        }

        [Test]
        public void WhenPriceIsMissing_ShouldReturnError()
        {
            var model = _fixture.Build<MealReceived>()
                .Without(x => x.Price)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Price);
        }

        [Test]
        public void WhenMealTypeIdIsMissing_ShouldReturnError()
        {
            var model = _fixture.Build<MealReceived>()
                .Without(x => x.MealTypeId)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.MealTypeId);
        }

        [Test]
        public void WhenRecipeIdIsMissing_ShouldReturnError()
        {
            var model = _fixture.Build<MealReceived>()
                .Without(x => x.RecipeId)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.RecipeId);
        }

        [Test]
        public void WhenPriceIsNegative_ShouldReturnError()
        {
            var model = _fixture.Build<MealReceived>()
                .With(x => x.Price, -1)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Price);
        }

        [Test]
        public void WhenRecipeIdIsNegative_ShouldReturnError()
        {
            var model = _fixture.Build<MealReceived>()
                .With(x => x.RecipeId, -1)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.RecipeId);
        }

        [Test]
        public void WhenMealTypeIdOutOfRange_ShouldReturnError()
        {
            var model = _fixture.Build<MealReceived>()
                .With(x => x.MealTypeId, (MealType)0)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.MealTypeId);
        }

        [Test]
        public void WhenNameLenghtExceedsMaximum_ShouldReturnError()
        {
            var model = _fixture.Build<MealReceived>()
                .With(x => x.Name, "namenamenamenamenamenamenamenamenamenamenamenamenamenamename")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Test]
        public void WhenNameFormatInvalid_ShouldReturnError()
        {
            var model = _fixture.Build<MealReceived>()
                .With(x => x.Name, " name")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Test]
        public void WhenDescriptionLenghtExceedsMaximum_ShouldReturnError()
        {
            var model = _fixture.Build<MealReceived>()
                .With(x => x.Description, StringExtensions.StringOfLength(1025))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Description);
        }
    }
}
