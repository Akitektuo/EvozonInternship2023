using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Meals;
using MealPlan.API.Requests.Menus;
using MealPlan.Data.Models.Meals;
using MealPlan.Data.Models.Menus;
using MealPlan.UnitTests.Utils;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MealPlan.UnitTests.Api.Validators.Menus
{
    [TestFixture]
    public class AddMenuValidatorTests
    {
        private AddMenuRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _fixture = new Fixture();
            _validator = new AddMenuRequestValidator();
        }

        [Test]
        public void WhenNameMissing_ShouldReturnError()
        {
            var model = _fixture.Build<AddMenuRequest>()
                .Without(x => x.Name)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Test]
        public void WhenNameLongerThanMax_ShouldReturnError()
        {
            var model = _fixture.Build<AddMenuRequest>()
                .With(x => x.Name, StringUtils.RandomString(51))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Test]
        public void WhenNameShorterThanMin_ShouldReturnError()
        {
            var model = _fixture.Build<AddMenuRequest>()
                .With(x => x.Name, "a")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Test]
        public void WhenDescriptionMissing_ShouldReturnError()
        {
            var model = _fixture.Build<AddMenuRequest>()
                .Without(x => x.Description)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Description);
        }

        [Test]
        public void WhenDescriptionLongerThanMax_ShouldReturnError()
        {
            var model = _fixture.Build<AddMenuRequest>()
                .With(x => x.Description, StringUtils.RandomString(300))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Description);
        }

        [Test]
        public void WhenDescriptionShorterThanMin_ShouldReturnError()
        {
            var model = _fixture.Build<AddMenuRequest>()
                .With(x => x.Description, "a")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Description);
        }

        [Test]
        public void WhenMenuCategoryIdNull_ShouldReturnError()
        {
            var model = _fixture.Build<AddMenuRequest>()
                .Without(x => x.CategoryId)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.CategoryId);
        }

        [Test]
        public void WhenMenuCategoryIdZero_ShouldReturnError()
        {
            var model = _fixture.Build<AddMenuRequest>()
                .With(x => x.CategoryId, (MenuCategory)0)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.CategoryId);
        }

        [Test]
        public void WhenMenuCategoryIdNegative_ShouldReturnError()
        {
            var model = _fixture.Build<AddMenuRequest>()
                .With(x => x.CategoryId, (MenuCategory)(-7))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.CategoryId);
        }


        [Test]
        public void WhenMenuCategoryNotValid_ShouldReturnError()
        {
            var model = _fixture.Build<AddMenuRequest>()
                .With(x => x.CategoryId, (MenuCategory)12345678)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.CategoryId);
        }

        [Test]
        public void WhenMealsMissing_ShouldReturnError()
        {
            var model = _fixture.Build<AddMenuRequest>()
                .Without(x => x.Meals)
                .Create();
            
             _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Meals);
        }

        [Test]
        public void WhenMealCountNotCorrect_ShouldReturnError()
        {
            var meals = new List<AddMealRequest> 
            { 
                new AddMealRequest
                {
                    Name = "Test",
                    Price = 1,
                    MealTypeId = MealType.Snack,
                    RecipeId = 1
                }
            };

            var model = _fixture.Build<AddMenuRequest>()
                .With(x => x.Meals, meals)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Meals);
        }

        [Test]
        public void WhenMealsContainNullMeal_ShouldReturnError()
        {
            var meals = new List<AddMealRequest>
            {
                new AddMealRequest
                {
                    Name = "Test",
                    Price = 1,
                    MealTypeId = MealType.Snack,
                    RecipeId = 1
                },
                null,
                null,
                null,
                null
            };

            var model = _fixture.Build<AddMenuRequest>()
                .With(x => x.Meals, meals)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Meals);
        }

        [Test]
        public void WhenMealTypesNotDistinct_ShouldReturnError()
        {
            var meals = new List<AddMealRequest>
            {
                new AddMealRequest
                {
                    Name = "Test",
                    Price = 1,
                    MealTypeId = MealType.Snack,
                    RecipeId = 1
                },
                new AddMealRequest
                {
                    Name = "Test",
                    Price = 1,
                    MealTypeId = MealType.Dinner,
                    RecipeId = 2
                },
                new AddMealRequest
                {
                    Name = "Test",
                    Price = 1,
                    MealTypeId = MealType.Dessert,
                    RecipeId = 3
                },
                new AddMealRequest
                {
                    Name = "Test",
                    Price = 1,
                    MealTypeId = MealType.Lunch,
                    RecipeId = 4
                },
                new AddMealRequest
                {
                    Name = "Test",
                    Price = 1,
                    MealTypeId = MealType.Snack,
                    RecipeId = 5
                }
            };

            var model = _fixture.Build<AddMenuRequest>()
                .With(x => x.Meals, meals)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Meals);
        }

        [Test]
        public void WhenMealRecipeIdsNotDistinct_ShouldReturnError()
        {
            var meals = new List<AddMealRequest>
            {
                new AddMealRequest
                {
                    Name = "Test",
                    Price = 1,
                    MealTypeId = MealType.Breakfast,
                    RecipeId = 1
                },
                new AddMealRequest
                {
                    Name = "Test",
                    Price = 1,
                    MealTypeId = MealType.Dinner,
                    RecipeId = 1
                },
                new AddMealRequest
                {
                    Name = "Test",
                    Price = 1,
                    MealTypeId = MealType.Dessert,
                    RecipeId = 3
                },
                new AddMealRequest
                {
                    Name = "Test",
                    Price = 1,
                    MealTypeId = MealType.Lunch,
                    RecipeId = 4
                },
                new AddMealRequest
                {
                    Name = "Test",
                    Price = 1,
                    MealTypeId = MealType.Snack,
                    RecipeId = 5
                }
            };

            var model = _fixture.Build<AddMenuRequest>()
                .With(x => x.Meals, meals)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Meals);
        }
    }
}