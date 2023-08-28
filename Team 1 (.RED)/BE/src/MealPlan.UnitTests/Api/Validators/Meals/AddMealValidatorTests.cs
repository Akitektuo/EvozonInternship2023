using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Meals;
using MealPlan.Data.Models.Meals;
using MealPlan.UnitTests.Utils;
using NUnit.Framework;

namespace MealPlan.UnitTests.Api.Validators.Meals
{
    [TestFixture]
    public class AddMealValidatorTests
    {
        private AddMealRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _fixture = new Fixture();
            _validator = new AddMealRequestValidator();
        }

        [Test]
        public void WhenNameMissing_ShouldReturnError()
        {
            var model = _fixture.Build<AddMealRequest>()
                .Without(x => x.Name)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Test]
        public void WhenNameLongerThanMax_ShouldReturnError()
        {
            var model = _fixture.Build<AddMealRequest>()
                .With(x => x.Name, StringUtils.RandomString(51))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Test]
        public void WhenNameShorterThanMin_ShouldReturnError()
        {
            var model = _fixture.Build<AddMealRequest>()
                .With(x => x.Name, "a")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Test]
        public void WhenPriceNull_ShouldReturnError()
        {
            var model = _fixture.Build<AddMealRequest>()
                .Without(x => x.Price)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Price);
        }

        [Test]
        public void WhenPriceZero_ShouldReturnError()
        {
            var model = _fixture.Build<AddMealRequest>()
                .With(x => x.Price, 0)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Price);
        }

        [Test]
        public void WhenPriceNegative_ShouldReturnError()
        {
            var model = _fixture.Build<AddMealRequest>()
                .With(x => x.Price, -5)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Price);
        }

        [Test]
        public void WhenMealTypeIdNull_ShouldReturnError()
        {
            var model = _fixture.Build<AddMealRequest>()
                .Without(x => x.MealTypeId)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.MealTypeId);
        }

        [Test]
        public void WhenMealTypeIdZero_ShouldReturnError()
        {
            var model = _fixture.Build<AddMealRequest>()
                .With(x => x.MealTypeId, (MealType)0)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.MealTypeId);
        }

        [Test]
        public void WhenMealTypeIdNegative_ShouldReturnError()
        {
            var model = _fixture.Build<AddMealRequest>()
                .With(x => x.MealTypeId, (MealType)(-7))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.MealTypeId);
        }

        [Test]
        public void WhenMealTypeIdNotValid_ShouldReturnError()
        {
            var model = _fixture.Build<AddMealRequest>()
                .With(x => x.MealTypeId, (MealType)12345678)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.MealTypeId);
        }

        [Test]
        public void WhenRecipeIdNull_ShouldReturnError()
        {
            var model = _fixture.Build<AddMealRequest>()
                .Without(x => x.RecipeId)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.RecipeId);
        }

        [Test]
        public void WhenRecipeIdZero_ShouldReturnError()
        {
            var model = _fixture.Build<AddMealRequest>()
                .With(x => x.RecipeId, 0)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.RecipeId);
        }

        [Test]
        public void WhenRecipeIdNegative_ShouldReturnError()
        {
            var model = _fixture.Build<AddMealRequest>()
                .With(x => x.RecipeId, -7)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.RecipeId);
        }
    }
}