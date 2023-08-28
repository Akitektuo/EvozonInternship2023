using FluentAssertions;
using FluentValidation;
using MealPlan.Business.Exceptions;
using MealPlan.Business.Meals.Commands;
using MealPlan.Business.Menus.Commands;
using MealPlan.Business.Menus.Handlers;
using MealPlan.Data;
using MealPlan.Data.Models.Meals;
using MealPlan.Data.Models.Menus;
using MealPlan.Data.Models.Recipes;
using MealPlan.Data.Models.Users;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Business.Menus.Handlers
{
    [TestFixture]
    public class AddMenuCommandHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private AddMenuCommandHandler _handler;
        private AddMenuCommand _successfulRequest;
        private AddMenuCommand _recipesNotExistRequest;
        private AddMenuCommand _recipesAlreadyUsedRequest;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new AddMenuCommandHandler(_context.Object);

            CreateCommands();
            SetupContext();
        }

        [TearDown]
        public void Clean()
        {
            _context = null;
            _handler = null;
        }

        [Test]
        public async Task ShouldAddMenu()
        {
            _context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _handler.Handle(_successfulRequest, new CancellationToken());

            result.Should().BeTrue();
        }

        [Test]
        public async Task WhenRecipeNotExists_ShouldThrowCustomApplicationException()
        {
            Func<Task> action = async () => await _handler.Handle(_recipesNotExistRequest, new CancellationToken());

            await action.Should()
                .ThrowAsync<CustomApplicationException>()
                .WithMessage(JsonSerializer.Serialize(
                    new CustomException
                    {
                        Message = "Recipe does not exist",
                        Code = (int)ErrorCode.MealRecipeNotFound
                    }));
        }

        [Test]
        public async Task WhenRecipeAlreadyUsed_ShouldThrowCustomApplicationException()
        {
            Func<Task> action = async () => await _handler.Handle(_recipesAlreadyUsedRequest, new CancellationToken());

            await action.Should()
                .ThrowAsync<CustomApplicationException>()
                .WithMessage(JsonSerializer.Serialize(
                    new CustomException
                    {
                        Message = "Recipe already used",
                        Code = (int)ErrorCode.MealRecipeAlreadyUsed
                    }));
        }

        private void CreateCommands()
        {
            var validMealList = new List<AddMealCommand>
            {
                new AddMealCommand
                {
                    Name = "Meal1",
                    Price = 1,
                    MealTypeId = MealType.Breakfast,
                    RecipeId = 1
                },
                new AddMealCommand
                {
                    Name = "Meal2",
                    Price = 1,
                    MealTypeId = MealType.Lunch,
                    RecipeId = 2
                },
                new AddMealCommand
                {
                    Name = "Meal3",
                    Price = 1,
                    MealTypeId = MealType.Dinner,
                    RecipeId = 3
                },
                new AddMealCommand
                {
                    Name = "Meal4",
                    Price = 1,
                    MealTypeId = MealType.Dessert,
                    RecipeId = 4
                },
                new AddMealCommand
                {
                    Name = "Meal5",
                    Price = 1,
                    MealTypeId = MealType.Snack,
                    RecipeId = 5
                }
            };

            var recipeDoesNotExistMealList = new List<AddMealCommand>
            {
                new AddMealCommand
                {
                    Name = "Meal1",
                    Price = 1,
                    MealTypeId = MealType.Breakfast,
                    RecipeId = 404
                },
                new AddMealCommand
                {
                    Name = "Meal2",
                    Price = 1,
                    MealTypeId = MealType.Lunch,
                    RecipeId = 2
                },
                new AddMealCommand
                {
                    Name = "Meal3",
                    Price = 1,
                    MealTypeId = MealType.Dinner,
                    RecipeId = 3
                },
                new AddMealCommand
                {
                    Name = "Meal4",
                    Price = 1,
                    MealTypeId = MealType.Dessert,
                    RecipeId = 4
                },
                new AddMealCommand
                {
                    Name = "Meal5",
                    Price = 1,
                    MealTypeId = MealType.Snack,
                    RecipeId = 5
                }
            };

            var recipeAlreadyUsedMealList = new List<AddMealCommand>
            {
                new AddMealCommand
                {
                    Name = "Meal1",
                    Price = 1,
                    MealTypeId = MealType.Breakfast,
                    RecipeId = 403
                },
                new AddMealCommand
                {
                    Name = "Meal2",
                    Price = 1,
                    MealTypeId = MealType.Lunch,
                    RecipeId = 2
                },
                new AddMealCommand
                {
                    Name = "Meal3",
                    Price = 1,
                    MealTypeId = MealType.Dinner,
                    RecipeId = 3
                },
                new AddMealCommand
                {
                    Name = "Meal4",
                    Price = 1,
                    MealTypeId = MealType.Dessert,
                    RecipeId = 4
                },
                new AddMealCommand
                {
                    Name = "Meal5",
                    Price = 1,
                    MealTypeId = MealType.Snack,
                    RecipeId = 5
                }
            };

            _successfulRequest = new AddMenuCommand
            {
                Name = "Name1",
                Description = "Description1",
                CategoryId = MenuCategory.FoodLover,
                Meals = validMealList
            };

            _recipesNotExistRequest = new AddMenuCommand
            {
                Name = "Name1",
                Description = "Description1",
                CategoryId = MenuCategory.FoodLover,
                Meals = recipeDoesNotExistMealList
            };

            _recipesAlreadyUsedRequest = new AddMenuCommand
            {
                Name = "Name1",
                Description = "Description1",
                CategoryId = MenuCategory.FoodLover,
                Meals = recipeAlreadyUsedMealList
            };
        }

        private void SetupContext()
        {
            var menus = new List<Menu> { };
            var recipes = new List<Recipe>
            {   new Recipe
                {
                    Id = 1,
                    Description = "Description"
                },
                new Recipe
                {
                    Id = 2,
                    Description = "Description"
                },
                new Recipe
                {
                    Id = 3,
                    Description = "Description"
                },
                new Recipe
                {
                    Id = 4,
                    Description = "Description"
                },
                new Recipe
                {
                    Id = 5,
                    Description = "Description"
                },
                new Recipe
                {
                    Id = 403,
                    Description = "Description",
                    Meal = new Meal
                    {
                        Id = 403
                    }
                }
            };

            var meals = new List<Meal>
            {
                new Meal
                {
                    Id = 403,
                    Name = "Name",
                    Price = 1,
                    MealTypeId = MealType.Snack,
                    RecipeId = 403
                }
            };

            var users = new List<User>
            {
                new User
                {
                    Id = 1,
                    FirstName = "Andrei",
                    LastName = "Baciu",
                    Email = "admin@gmail.com",
                    Password = "password",
                    RoleId = Role.Admin,
                },
                new User
                {
                    Id = 2,
                    FirstName = "Plebeul",
                    LastName = "Cartof",
                    Email = "user@gmail.com",
                    Password = "password",
                    RoleId = Role.User,
                }
            };

            _context.Setup(c => c.Menus).ReturnsDbSet(menus);
            _context.Setup(c => c.Recipes).ReturnsDbSet(recipes);
            _context.Setup(c => c.Meals).ReturnsDbSet(meals);
            _context.Setup(c => c.Users).ReturnsDbSet(users);
        }
    }
}