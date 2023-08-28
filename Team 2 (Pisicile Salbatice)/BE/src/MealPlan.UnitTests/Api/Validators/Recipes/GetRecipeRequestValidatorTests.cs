using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Recipes;
using NUnit.Framework;

namespace MealPlan.UnitTests.Api.Validators.Recipes
{
    [TestFixture]
    public class GetRecipeRequestValidatorTests
    {
        private GetRecipeRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _validator = new GetRecipeRequestValidator();
            _fixture = new Fixture();
        }

        [Test]
        public void WhenIdPresent_ShouldNotReturnError()
        {
            var model = _fixture.Build<GetRecipeRequest>()
                .Create();

            _validator.TestValidate(model).ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void WhenIdNotPresent_ShouldReturnError()
        {
            var model = _fixture.Build<GetRecipeRequest>()
                .Without(x => x.RecipeId)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.RecipeId);
        }

        [Test]
        public void WhenIdNegative_ShouldReturnError()
        {
            var model = _fixture.Build<GetRecipeRequest>()
                .With(x => x.RecipeId, -1)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.RecipeId);
        }
    }
}
