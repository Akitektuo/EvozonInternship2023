using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Menus;
using MealPlan.Data.Models.Menus;
using NUnit.Framework;

namespace MealPlan.UnitTests.Api.Validators.Menus
{
    [TestFixture]
    public class GetSuggestedMenuRequestValidatorTests
    {
        private GetSuggestedMenuRequestValidator _validator;
        private IFixture _fixture;
        
        [SetUp]
        public void Init()
        {
            _validator = new GetSuggestedMenuRequestValidator();
            _fixture = new Fixture();
        }

        [Test]
        public void WhenModelIsValid_ShouldNotHaveErrors()
        {
            var model = _fixture.Build<GetSuggestedMenuRequest>()
                .With(x => x.PriceSuggestion, 19)
                .With(x => x.CategoryId, MenuCategory.FoodLover)
                .Create();

            _validator.TestValidate(model).ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void WhenPriceSuggestionIsEmpty_ShouldReturnError()
        {
            var model = _fixture.Build<GetSuggestedMenuRequest>()
                .Without(x => x.PriceSuggestion)
                .With(x => x.CategoryId, MenuCategory.FoodLover)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.PriceSuggestion);
        }

        [Test]
        public void WhenPriceSuggestionTooLow_ShouldReturnError()
        {
            var model = _fixture.Build<GetSuggestedMenuRequest>()
                .With(x => x.PriceSuggestion, 1)
                .With(x => x.CategoryId, MenuCategory.FoodLover)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.PriceSuggestion);
        }

        [Test]
        public void WhenPriceSuggestionTooHigh_ShouldReturnError()
        {
            var model = _fixture.Build<GetSuggestedMenuRequest>()
                .With(x => x.PriceSuggestion, 1001)
                .With(x => x.CategoryId, MenuCategory.FoodLover)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.PriceSuggestion);
        }

        [Test]
        public void WhenCategoryIdIsEmpty_ShouldReturnError()
        {
            var model = _fixture.Build<GetSuggestedMenuRequest>()
                .With(x => x.PriceSuggestion, 19)
                .Without(x => x.CategoryId)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.CategoryId);
        }

        [Test]
        public void WhenCategoryIdIsNotInEnum_ShouldReturnError()
        {
            var model = _fixture.Build<GetSuggestedMenuRequest>()
                .With(x => x.PriceSuggestion, 19)
                .With(x => x.CategoryId, (MenuCategory)103)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.CategoryId);
        }
    }
}