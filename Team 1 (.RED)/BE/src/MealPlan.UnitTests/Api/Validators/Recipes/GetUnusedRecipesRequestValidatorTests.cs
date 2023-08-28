using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Recipes;
using NUnit.Framework;

namespace MealPlan.UnitTests.Api.Validators.Recipes
{
    [TestFixture]
    public class GetUnusedRecipesRequestValidatorTests
    {
        private GetUnusedRecipesRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _validator = new GetUnusedRecipesRequestValidator();
            _fixture = new Fixture();
        }

        [Test]
        public void WhenPageNumberSmallerThanZero_ShouldReturnError()
        {
            var model = _fixture.Build<GetUnusedRecipesRequest>()
                .With(x => x.PageNumber, -1)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.PageNumber);
        }

        [Test]
        public void WhenPageSizeSmallerThanZero_ShouldReturnError()
        {
            var model = _fixture.Build<GetUnusedRecipesRequest>()
                .With(x => x.PageSize, -1)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.PageSize);
        }

        [Test]
        public void WhenPageNumberEqualToZero_ShouldReturnError()
        {
            var model = _fixture.Build<GetUnusedRecipesRequest>()
                .With(x => x.PageNumber, 0)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.PageNumber);
        }

        [Test]
        public void WhenPageSizeEqualToZero_ShouldReturnError()
        {
            var model = _fixture.Build<GetUnusedRecipesRequest>()
                .With(x => x.PageSize, 0)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.PageSize);
        }
    }
}