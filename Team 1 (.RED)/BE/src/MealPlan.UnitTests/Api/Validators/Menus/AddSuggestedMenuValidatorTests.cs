using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Meals;
using MealPlan.API.Requests.Menus;
using MealPlan.API.Requests.Recipes;
using MealPlan.Data.Models.Meals;
using MealPlan.Data.Models.Menus;
using MealPlan.UnitTests.Utils;
using NUnit.Framework;
using System.Collections.Generic;

namespace MealPlan.UnitTests.Api.Validators.Menus
{
    [TestFixture]
    public class AddSuggestedMenuValidatorTests
    {
        private AddSuggestedMenuRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _fixture = new Fixture();
            _validator = new AddSuggestedMenuRequestValidator();
        }

        [Test]
        public void WhenNameMissing_ShouldReturnError()
        {
            var model = _fixture.Build<AddSuggestedMenuRequest>()
                .Without(x => x.Name)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Test]
        public void WhenNameLongerThanMax_ShouldReturnError()
        {
            var model = _fixture.Build<AddSuggestedMenuRequest>()
                .With(x => x.Name, StringUtils.RandomString(51))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Test]
        public void WhenNameShorterThanMin_ShouldReturnError()
        {
            var model = _fixture.Build<AddSuggestedMenuRequest>()
                .With(x => x.Name, "a")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Test]
        public void WhenDescriptionMissing_ShouldReturnError()
        {
            var model = _fixture.Build<AddSuggestedMenuRequest>()
                .Without(x => x.Description)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Description);
        }

        [Test]
        public void WhenDescriptionLongerThanMax_ShouldReturnError()
        {
            var model = _fixture.Build<AddSuggestedMenuRequest>()
                .With(x => x.Description, StringUtils.RandomString(300))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Description);
        }

        [Test]
        public void WhenDescriptionShorterThanMin_ShouldReturnError()
        {
            var model = _fixture.Build<AddSuggestedMenuRequest>()
                .With(x => x.Description, "a")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Description);
        }

        [Test]
        public void WhenMenuCategoryIdNull_ShouldReturnError()
        {
            var model = _fixture.Build<AddSuggestedMenuRequest>()
                .Without(x => x.CategoryId)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.CategoryId);
        }

        [Test]
        public void WhenMenuCategoryIdZero_ShouldReturnError()
        {
            var model = _fixture.Build<AddSuggestedMenuRequest>()
                .With(x => x.CategoryId, (MenuCategory)0)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.CategoryId);
        }

        [Test]
        public void WhenMenuCategoryIdNegative_ShouldReturnError()
        {
            var model = _fixture.Build<AddSuggestedMenuRequest>()
                .With(x => x.CategoryId, (MenuCategory)(-7))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.CategoryId);
        }


        [Test]
        public void WhenMenuCategoryNotValid_ShouldReturnError()
        {
            var model = _fixture.Build<AddSuggestedMenuRequest>()
                .With(x => x.CategoryId, (MenuCategory)12345678)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.CategoryId);
        }

        [Test]
        public void WhenMealsMissing_ShouldReturnError()
        {
            var model = _fixture.Build<AddSuggestedMenuRequest>()
                .Without(x => x.Meals)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Meals);
        }

        [Test]
        public void WhenMealCountNotCorrect_ShouldReturnError()
        {
            var meals = new List<AddSuggestedMealRequest>
            {
                new AddSuggestedMealRequest
                {
                    Name = "Test",
                    Price = 1,
                    MealTypeId = (int)MealType.Snack,
                    Recipe = new AddSuggestedRecipeRequest
                    {
                        Description = "Test",
                        Name = "Test",
                        Ingredients = new List<string> { "Test" }
                    }
                }
            };

            var model = _fixture.Build<AddSuggestedMenuRequest>()
                .With(x => x.Meals, meals)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Meals);
        }

        [Test]
        public void WhenMealsContainNullMeal_ShouldReturnError()
        {
            var meals = new List<AddSuggestedMealRequest>
            {
                new AddSuggestedMealRequest
                {
                    Name = "Test",
                    Price = 1,
                    MealTypeId = (int)MealType.Snack,
                    Recipe = new AddSuggestedRecipeRequest
                    {
                        Description = "Test",
                        Name = "Test",
                        Ingredients = new List<string> { "Test" }
                    }
                },
                null,
                null,
                null,
                null
            };

            var model = _fixture.Build<AddSuggestedMenuRequest>()
                .With(x => x.Meals, meals)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Meals);
        }

        [Test]
        public void WhenMealTypesNotDistinct_ShouldReturnError()
        {
            var meals = new List<AddSuggestedMealRequest>
            {
                new AddSuggestedMealRequest
                {
                    Name = "Test",
                    Price = 1,
                    MealTypeId = (int)MealType.Snack,
                    Recipe = new AddSuggestedRecipeRequest
                    {
                        Description = "Test",
                        Name = "Test",
                        Ingredients = new List<string> { "Test" }
                    }
                },
                new AddSuggestedMealRequest
                {
                    Name = "Test",
                    Price = 1,
                    MealTypeId = (int)MealType.Dinner,
                    Recipe = new AddSuggestedRecipeRequest
                    {
                        Description = "Test",
                        Name = "Test",
                        Ingredients = new List<string> { "Test" }
                    }
                },
                new AddSuggestedMealRequest
                {
                    Name = "Test",
                    Price = 1,
                    MealTypeId = (int)MealType.Dessert,
                    Recipe = new AddSuggestedRecipeRequest
                    {
                        Description = "Test",
                        Name = "Test",
                        Ingredients = new List<string> { "Test" }
                    }
                },
                new AddSuggestedMealRequest
                {
                    Name = "Test",
                    Price = 1,
                    MealTypeId = (int)MealType.Lunch,
                    Recipe = new AddSuggestedRecipeRequest
                    {
                        Description = "Test",
                        Name = "Test",
                        Ingredients = new List<string> { "Test" }
                    }
                },
                new AddSuggestedMealRequest
                {
                    Name = "Test",
                    Price = 1,
                    MealTypeId = (int)MealType.Snack,
                    Recipe = new AddSuggestedRecipeRequest
                    {
                        Description = "Test",
                        Name = "Test",
                        Ingredients = new List<string> { "Test" }
                    }
                }
            };

            var model = _fixture.Build<AddSuggestedMenuRequest>()
                .With(x => x.Meals, meals)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Meals);
        }

        [Test]
        public void WhenMealRecipeNamesNotDistinct_ShouldReturnError()
        {
            var meals = new List<AddSuggestedMealRequest>
            {
                new AddSuggestedMealRequest
                {
                    Name = "Test",
                    Price = 1,
                    MealTypeId = (int)MealType.Breakfast,
                    Recipe = new AddSuggestedRecipeRequest
                    {
                        Description = "Test",
                        Name = "Test1",
                        Ingredients = new List<string> { "Test" }
                    }
                },
                new AddSuggestedMealRequest
                {
                    Name = "Test",
                    Price = 1,
                    MealTypeId = (int)MealType.Dinner,
                    Recipe = new AddSuggestedRecipeRequest
                    {
                        Description = "Test",
                        Name = "Test2",
                        Ingredients = new List<string> { "Test" }
                    }
                },
                new AddSuggestedMealRequest
                {
                    Name = "Test",
                    Price = 1,
                    MealTypeId = (int)MealType.Dessert,
                    Recipe = new AddSuggestedRecipeRequest
                    {
                        Description = "Test",
                        Name = "Test3",
                        Ingredients = new List<string> { "Test" }
                    }
                },
                new AddSuggestedMealRequest
                {
                    Name = "Test",
                    Price = 1,
                    MealTypeId = (int)MealType.Lunch,
                    Recipe = new AddSuggestedRecipeRequest
                    {
                        Description = "Test",
                        Name = "Test4",
                        Ingredients = new List<string> { "Test" }
                    }
                },
                new AddSuggestedMealRequest
                {
                    Name = "Test",
                    Price = 1,
                    MealTypeId = (int)MealType.Snack,
                    Recipe = new AddSuggestedRecipeRequest
                    {
                        Description = "Test",
                        Name = "Test1",
                        Ingredients = new List<string> { "Test" }
                    }
                }
            };

            var model = _fixture.Build<AddSuggestedMenuRequest>()
                .With(x => x.Meals, meals)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Meals);
        }
    }
}