using AutoFixture;
using MealPlan.API.Requests.Menus;
using NUnit.Framework;
using System.Collections.Generic;
using FluentValidation.TestHelper;
using MealPlan.Data.Models.Meals;

namespace MealPlan.UnitTests.Api.Validators.Menus
{
    [TestFixture]
    public class AddMenuRequestValidatorTests
    {
        private AddMenuRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _validator = new AddMenuRequestValidator();
            _fixture = new Fixture();
        }

        [Test]
        public void WhenGoodData_ShouldNotReturnError()
        {
            var model = _fixture.Build<AddMenuRequest>()
                .With(x => x.MenuName, "menuName")
                .With(x => x.MenuTypeId, MenuType.Fitness)
                .With(x => x.Meals, new List<MealReceived>() {
                    new MealReceived { Description = "desc", Name = "names", Price = 10, MealTypeId = MealType.Breakfast, RecipeId = 1 },
                    new MealReceived { Description = "desc", Name = "namess", Price = 10, MealTypeId = MealType.Dinner, RecipeId = 2 },
                    new MealReceived { Description = "desc", Name = "namesss", Price = 10, MealTypeId = MealType.Lunch, RecipeId = 3 },
                    new MealReceived { Description = "desc", Name = "namessss", Price = 10, MealTypeId = MealType.Dessert, RecipeId = 4 },
                    new MealReceived { Description = "desc", Name = "namesssss", Price = 10, MealTypeId = MealType.Snack, RecipeId = 5 }})
                .Create();

            _validator.TestValidate(model).ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void WhenNotEnoughMeals_ShouldThrowError()
        {
            var model = _fixture.Build<AddMenuRequest>()
                .With(x => x.MenuName, "menuName")
                .With(x => x.MenuTypeId, MenuType.Fitness)
                .With(x => x.Meals, new List<MealReceived>() {
                    new MealReceived { Description = "desc", Name = "name", Price = 10, MealTypeId = MealType.Breakfast, RecipeId = 1 }})
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Meals);

        }

        [Test]
        public void WhenDuplicateMealTypeId_ShouldThrowError()
        {
            var model = _fixture.Build<AddMenuRequest>()
                .With(x => x.MenuName, "menuName")
                .With(x => x.MenuTypeId, MenuType.Fitness)
                .With(x => x.Meals, new List<MealReceived>() {
                    new MealReceived { Description = "desc", Name = "name", Price = 10, MealTypeId = MealType.Breakfast, RecipeId = 1 },
                    new MealReceived { Description = "desc", Name = "name", Price = 10, MealTypeId = MealType.Dinner, RecipeId = 2 },
                    new MealReceived { Description = "desc", Name = "name", Price = 10, MealTypeId = MealType.Dinner, RecipeId = 3 },
                    new MealReceived { Description = "desc", Name = "name", Price = 10, MealTypeId = MealType.Dessert, RecipeId = 4 },
                    new MealReceived { Description = "desc", Name = "name", Price = 10, MealTypeId = MealType.Snack, RecipeId = 5 }})
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Meals);
        }

        [Test]
        public void WhenMealsListContainsInvalidMeal_ShouldThrowError()
        {
            var model = _fixture.Build<AddMenuRequest>()
                .With(x => x.MenuName, "menuName")
                .With(x => x.MenuTypeId, MenuType.Fitness)
                .With(x => x.Meals, new List<MealReceived>() {
                    new MealReceived { Description = "desc", Name = "name", Price = 10, MealTypeId = MealType.Breakfast, RecipeId = 1 },
                    new MealReceived { Description = "", Name = "name", Price = 10, MealTypeId = MealType.Dinner, RecipeId = 2 },
                    new MealReceived { Description = "desc", Name = "name", Price = 10, MealTypeId = MealType.Lunch, RecipeId = 3 },
                    new MealReceived { Description = "desc", Name = "name", Price = 10, MealTypeId = MealType.Dessert, RecipeId = 4 },
                    new MealReceived { Description = "desc", Name = "name", Price = 10, MealTypeId = MealType.Snack, RecipeId = 5 }})
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Meals);
        }

        [Test]
        public void WhenMenuNameIsMissing_ShouldThrowError()
        {
            var model = _fixture.Build<AddMenuRequest>()
                .Without(x => x.MenuName)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.MenuName);
        }

        [Test]
        public void WhenMenuTypeIdMissing_ShouldThrowError()
        {
            var model = _fixture.Build<AddMenuRequest>()
                .Without(x => x.MenuTypeId)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.MenuTypeId);
        }

        [Test]
        public void WhenNameLenghtExceedsMaximum_ShouldReturnError()
        {
            var model = _fixture.Build<AddMenuRequest>()
                .With(x => x.MenuName, "namenamenamenamenamenamenamenamenamenamenamenamenamenamename")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.MenuName);
        }

        [Test]
        public void WhenNameFormatInvalid_ShouldReturnError()
        {
            var model = _fixture.Build<AddMenuRequest>()
                .With(x => x.MenuName, " name")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.MenuName);
        }

        [Test]
        public void WhenDuplicateMealsName_ShouldReturnError()
        {
            var model = _fixture.Build<AddMenuRequest>()
                .With(x => x.MenuName, "name")
                .With(x => x.Meals, new List<MealReceived>()
                {
                    new MealReceived { Description = "desc", Name = "name", Price = 10, MealTypeId = MealType.Breakfast, RecipeId = 1 },
                    new MealReceived { Description = "desc", Name = "name", Price = 10, MealTypeId = MealType.Lunch, RecipeId = 2 }
                })
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Meals);
        }

        [Test]
        public void WhenDuplicateRecipeIds_ShouldReturnError()
        {
            var model = _fixture.Build<AddMenuRequest>()
                .With(x => x.MenuName, "name")
                .With(x => x.Meals, new List<MealReceived>()
                {
                    new MealReceived { Description = "desc", Name = "name1", Price = 10, MealTypeId = MealType.Breakfast, RecipeId = 1 },
                    new MealReceived { Description = "desc", Name = "name2", Price = 10, MealTypeId = MealType.Lunch, RecipeId = 1 }
                })
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Meals);
        }
    }
}
