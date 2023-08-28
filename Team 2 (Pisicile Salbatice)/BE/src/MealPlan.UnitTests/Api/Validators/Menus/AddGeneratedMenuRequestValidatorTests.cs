using AutoFixture;
using NUnit.Framework;
using System.Collections.Generic;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Menus.AddGeneratedMenu;
using MealPlan.Data.Models.Meals;
using System.Linq;
using MealPlan.UnitTests.Shared;

namespace MealPlan.UnitTests.Api.Validators.Menus
{
    [TestFixture]
    public class AddGeneratedMenuRequestValidatorTests
    {
        private AddGeneratedMenuRequestValidator _validator;
        private IFixture _fixture;
        private AddGeneratedMenuRequest _request;

        [SetUp]
        public void Init()
        {
            _validator = new AddGeneratedMenuRequestValidator();
            _fixture = new Fixture();

            CreateRequest();
        }

        [Test]
        public void WhenGoodData_ShouldNotReturnError()
        {
            _validator.TestValidate(_request).ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void WhenNotEnoughMeals_ShouldThrowError()
        {
            _request.Meals.RemoveAt(0);

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(model => model.Meals);

        }

        [Test]
        public void WhenDuplicateMealTypeId_ShouldThrowError()
        {
            _request.Meals.ElementAt(0).MealTypeId = _request.Meals.ElementAt(1).MealTypeId;

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(model => model.Meals);
        }

        [Test]
        public void WhenMealsListContainsInvalidMeal_ShouldThrowError()
        {
            _request.Meals.ElementAt(0).Price = -1;

            _validator.TestValidate(_request).ShouldHaveAnyValidationError();
        }

        [Test]
        public void WhenMealsListContainsInvalidName_ShouldThrowError()
        {
            _request.Meals.ElementAt(0).Name = StringExtensions.StringOfLength(51);

            _validator.TestValidate(_request).ShouldHaveAnyValidationError();
        }

        [Test]
        public void WhenMealsListContainsInvalidDescription_ShouldThrowError()
        {
            _request.Meals.ElementAt(0).Description = StringExtensions.StringOfLength(1025);

            _validator.TestValidate(_request).ShouldHaveAnyValidationError();
        }

        [Test]
        public void WhenRecipesListContainsInvalidName_ShouldThrowError()
        {
            _request.Meals.ElementAt(0).Recipe.Name = StringExtensions.StringOfLength(51);

            _validator.TestValidate(_request).ShouldHaveAnyValidationError();
        }

        [Test]
        public void WhenRecipesListContainsInvalidDescription_ShouldThrowError()
        {
            _request.Meals.ElementAt(0).Recipe.Description = StringExtensions.StringOfLength(501);

            _validator.TestValidate(_request).ShouldHaveAnyValidationError();
        }

        [Test]
        public void WhenMenuNameIsMissing_ShouldThrowError()
        {
            _request.Name = string.Empty;

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Test]
        public void WhenMenuTypeIdMissing_ShouldThrowError()
        {
            _request.MenuTypeId = (MenuType)0;

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(model => model.MenuTypeId);
        }

        [Test]
        public void WhenNameLenghtExceedsMaximum_ShouldReturnError()
        {
            _request.Name = StringExtensions.StringOfLength(100);

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Test]
        public void WhenDuplicateMealsName_ShouldReturnError()
        {
            _request.Meals.ElementAt(0).Name = _request.Meals.ElementAt(1).Name;

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(model => model.Meals);
        }

        private void CreateRequest()
        {
            _request = _fixture.Build<AddGeneratedMenuRequest>()
                .With(x => x.Name, "menuName")
                .With(x => x.MenuTypeId, MenuType.FoodLover)
                .With(x => x.Meals, new List<GeneratedMeal>() {
                    new GeneratedMeal { Description = "desc", Name = "name", Price = 10, MealTypeId = MealType.Snack,
                        Recipe = new GeneratedRecipe
                        {
                            Name = "test1",
                            Description = "test",
                            Ingredients = new List<string>
                            {
                                "Oua",
                                "Mere",
                                "Pere"
                            }
                        }},
                    new GeneratedMeal { Description = "desc", Name = "nametest", Price = 10, MealTypeId = MealType.Dessert,
                    Recipe = new GeneratedRecipe
                        {
                            Name = "test2",
                            Description = "test",
                            Ingredients = new List<string>
                            {
                                "Oua",
                                "Mere",
                                "Pere"
                            }
                        }},
                    new GeneratedMeal { Description = "desc", Name = "nametestt", Price = 10, MealTypeId = MealType.Dinner,
                    Recipe = new GeneratedRecipe
                        {
                            Name = "test3",
                            Description = "test",
                            Ingredients = new List<string>
                            {
                                "Oua",
                                "Mere",
                                "Pere"
                            }
                        }},
                    new GeneratedMeal { Description = "desc", Name = "nametesttt", Price = 10, MealTypeId = MealType.Lunch,
                    Recipe = new GeneratedRecipe
                        {
                            Name = "test4",
                            Description = "test",
                            Ingredients = new List<string>
                            {
                                "Oua",
                                "Mere",
                                "Pere"
                            }
                        }},
                    new GeneratedMeal { Description = "desc", Name = "nameetest", Price = 10, MealTypeId = MealType.Breakfast,
                    Recipe = new GeneratedRecipe
                        {
                            Name = "test5",
                            Description = "test",
                            Ingredients = new List<string>
                            {
                                "Oua",
                                "Mere",
                                "Pere"
                            }
                        }}})
                .Create();
        }
    }
}
