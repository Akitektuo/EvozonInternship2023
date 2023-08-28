using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Recipes;
using MealPlan.UnitTests.Shared;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;

namespace MealPlan.UnitTests.Api.Validators.Recipes
{
    [TestFixture]
    public class AddRecipeRequestValidatorTests
    {
        private AddRecipeRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _validator = new AddRecipeRequestValidator();
            _fixture = new Fixture();
        }

        [Test]
        public void WhenGoodCredentials_ShouldNotReturnError()
        {
            var model = _fixture.Build<AddRecipeRequest>()
                .With(x => x.Name, StringExtensions.StringOfLength(20))
                .With(x => x.Description, StringExtensions.StringOfLength(50))
                .With(x => x.IngredientIds, new List<int> { 1, 2 })
                .Create();

            _validator.TestValidate(model).ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void WhenNameMissing_ShouldReturnError()
        {
            var model = _fixture.Build<AddRecipeRequest>()
                .Without(x => x.Name)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Test]
        public void WhenNameTooLong_ShouldReturnError()
        {
            var model = _fixture.Build<AddRecipeRequest>()
                .With(x => x.Name, StringExtensions.StringOfLength(51))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Test]
        public void WhenDescriptionMissing_ShouldReturnError()
        {
            var model = _fixture.Build<AddRecipeRequest>()
                .Without(x => x.Description)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Description);
        }

        [Test]
        public void WhenDescriptionTooLong_ShouldReturnError()
        {
            var model = _fixture.Build<AddRecipeRequest>()
                .With(x => x.Description, StringExtensions.StringOfLength(501))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Description);
        }

        [Test]
        public void WhenIngredientsMissing_ShouldReturnError()
        {
            var model = _fixture.Build<AddRecipeRequest>()
                .Without(x => x.IngredientIds)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.IngredientIds);
        }

        [Test]
        public void WhenIngredientsIdIsLessThan1_ShouldReturnError()
        {
            var model = _fixture.Build<AddRecipeRequest>()
                .With(x => x.IngredientIds, new List<int> { 0 })
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.IngredientIds);
        }

    }
}
