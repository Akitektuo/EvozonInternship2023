using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Recipes;
using MealPlan.UnitTests.Utils;
using NUnit.Framework;
using System.Collections.Generic;

namespace MealPlan.UnitTests.Api.Validators.Recipes
{
    public class AddSuggestedRecipeValidatorTests
    {
        private AddSuggestedRecipeRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _fixture = new Fixture();
            _validator = new AddSuggestedRecipeRequestValidator();
        }

        [Test]
        public void WhenDescriptionMissing_ShouldReturnError()
        {
            var model = _fixture.Build<AddSuggestedRecipeRequest>()
                .Without(x => x.Description)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Description);
        }

        public void WhenDescriptionLongerThanMax_ShouldReturnError()
        {
            var model = _fixture.Build<AddSuggestedRecipeRequest>()
                .With(x => x.Description, StringUtils.RandomString(251))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Description);
        }

        [Test]
        public void WhenDescriptionShorterThanMin_ShouldReturnError()
        {
            var model = _fixture.Build<AddSuggestedRecipeRequest>()
                .With(x => x.Description, "aaaa")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Description);
        }

        [Test]
        public void WhenIngredientsEmpty_ShouldReturnError()
        {
            var model = _fixture.Build<AddSuggestedRecipeRequest>()
                .Without(x => x.Ingredients)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Ingredients);
        }

        [Test]
        public void WhenIngredientsContainEmptyIngredient_ShouldReturnError()
        {
            var model = _fixture.Build<AddSuggestedRecipeRequest>()
                .With(x => x.Ingredients, new List<string> { "eggs", "", null })
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Ingredients);
        }

        [Test]
        public void WhenNameMissing_ShouldReturnError()
        {
            var model = _fixture.Build<AddSuggestedRecipeRequest>()
                .Without(x => x.Name)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }

        public void WhenNameLongerThanMax_ShouldReturnError()
        {
            var model = _fixture.Build<AddSuggestedRecipeRequest>()
                .With(x => x.Name, StringUtils.RandomString(251))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Test]
        public void WhenNameShorterThanMin_ShouldReturnError()
        {
            var model = _fixture.Build<AddSuggestedRecipeRequest>()
                .With(x => x.Name, "aa")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }
    }
}