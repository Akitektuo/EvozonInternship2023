using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Recipes;
using MealPlan.UnitTests.Utils;
using NUnit.Framework;

namespace MealPlan.UnitTests.Api.Validators.Recipes
{
    [TestFixture]
    public class AddRecipeValidatorTests
    {
        private AddRecipeRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _fixture = new Fixture();
            _validator = new AddRecipeRequestValidator();
        }

        [Test]
        public void WhenDescriptionMissing_ShouldReturnError() 
        {
            var model = _fixture.Build<AddRecipeRequest>()
                .Without(x => x.Description)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Description);
        }

        public void WhenDescriptionLongerThanMax_ShouldReturnError()
        {
            var model = _fixture.Build<AddRecipeRequest>()
                .With(x => x.Description, StringUtils.RandomString(251))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Description);
        }

        [Test]
        public void WhenDescriptionShorterThanMin_ShouldReturnError()
        {
            var model = _fixture.Build<AddRecipeRequest>()
                .With(x => x.Description, "aaaa")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Description);
        }

        [Test]
        public void WhenIngredientsIdEmpty_ShouldReturnError()
        {
            var model = _fixture.Build<AddRecipeRequest>()
                .Without(x => x.IngredientIds)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.IngredientIds);
        }

        [Test]
        public void WhenNameMissing_ShouldReturnError()
        {
            var model = _fixture.Build<AddRecipeRequest>()
                .Without(x => x.Name)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }

        public void WhenNameLongerThanMax_ShouldReturnError()
        {
            var model = _fixture.Build<AddRecipeRequest>()
                .With(x => x.Name, StringUtils.RandomString(251))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Test]
        public void WhenNameShorterThanMin_ShouldReturnError()
        {
            var model = _fixture.Build<AddRecipeRequest>()
                .With(x => x.Name, "aa")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }
    }
}