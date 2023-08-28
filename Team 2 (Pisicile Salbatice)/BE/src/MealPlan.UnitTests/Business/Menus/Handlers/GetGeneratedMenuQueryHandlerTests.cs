using FluentAssertions;
using MealPlan.Business.Exceptions;
using MealPlan.Business.Menus.Handlers;
using MealPlan.Business.Menus.Models;
using MealPlan.Business.Menus.Queries;
using MealPlan.Data.Models.Meals;
using Moq;
using NUnit.Framework;
using OpenAI.Interfaces;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels.ResponseModels;
using OpenAI.ObjectModels.SharedModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Business.Menus.Handlers
{
    [TestFixture]
    public class GetGeneratedMenuQueryHandlerTests
    {
        private GetGeneratedMenuQueryHandler _handler;
        private GetGeneratedMenuQuery _request;
        private Mock<IOpenAIService> _openAIService;
        private string goodJsonText;
        private string badJsonText;

        [Test]
        public async Task ShouldReturnGeneratedMenuModel()
        {
            var result = await _handler.Handle(_request, new CancellationToken());

            result.Should().BeOfType<GeneratedMenuModel>();
            var expectedResult = new GeneratedMenuModel
            {
                Name = "Fitness Menu",
                MenuTypeId = MenuType.Fitness,
                Meals = new List<GeneratedMealModel>
                    {   new GeneratedMealModel
                        {
                            Name = "Protein Power Breakfast",
                            Description = "Start your day with a protein-packed breakfast",
                            Price = 10,
                            MealTypeId = MealType.Breakfast,
                            Recipe = new GeneratedRecipeModel
                            {
                                Name = "Protein Power Breakfast Recipe",
                                Description = "Fuel your morning with this nutritious and delicious breakfast recipe",
                                Ingredients = new List<string>
                                {
                                    "Eggs",
                                    "Spinach",
                                    "Tomatoes",
                                    "Avocado"
                                },
                            },
                        },
                        new GeneratedMealModel
                        {
                            Name = "Lean High-Protein Lunch",
                            Description = "A satisfying lunch option packed with lean proteins",
                            Price = 15,
                            MealTypeId = MealType.Lunch,
                            Recipe = new GeneratedRecipeModel
                            {
                                Name = "Lean High-Protein Lunch Recipe",
                                Description = "Enjoy this balanced and protein-rich lunch recipe for sustained energy",
                                Ingredients = new List<string>
                                {
                                    "Grilled chicken breast",
                                    "Quinoa",
                                    "Mixed vegetables"
                                },
                            },
                        },
                        new GeneratedMealModel
                        {
                            Name = "Fit and Healthy Dinner",
                            Description = "Nourish your body with a wholesome dinner choice",
                            Price = 20,
                            MealTypeId = MealType.Dinner,
                            Recipe = new GeneratedRecipeModel
                            {
                                Name = "Fit and Healthy Dinner Recipe",
                                Description = "Treat yourself to this delicious and nutritious dinner recipe",
                                Ingredients = new List<string>
                                {
                                    "Salmon fillet",
                                    "Brown rice",
                                    "Steamed broccoli"
                                },
                            },
                        },
                        new GeneratedMealModel
                        {
                            Name = "Energy-Boosting Snack",
                            Description = "Refuel your body with an energizing snack",
                            Price = 5,
                            MealTypeId = MealType.Snack,
                            Recipe = new GeneratedRecipeModel
                            {
                                Name = "Energy-Boosting Snack Recipe",
                                Description = "Satisfy your cravings with this tasty and nutritious snack recipe",
                                Ingredients = new List<string>
                                {
                                    "Greek yogurt",
                                    "Mixed berries",
                                    "Almonds"
                                },
                            },
                        },
                        new GeneratedMealModel
                        {
                            Name = "Protein-Packed Dessert",
                            Description = "Indulge in a guilt-free dessert with added protein",
                            Price = 10,
                            MealTypeId = MealType.Dessert,
                            Recipe = new GeneratedRecipeModel
                            {
                                Name = "Protein-Packed Dessert Recipe",
                                Description = "Treat yourself to this delicious and healthy dessert recipe",
                                Ingredients = new List<string>
                                {
                                    "Chocolate protein powder",
                                    "Banana",
                                    "Almond milk"
                                },
                            },
                        }
                    }
            };

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task ShouldThrowDeserializationError()
        {
            _openAIService.Setup(x => x.ChatCompletion.CreateCompletion(It.IsAny<ChatCompletionCreateRequest>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ChatCompletionCreateResponse()
                {
                    Choices = new List<ChatChoiceResponse> { new ChatChoiceResponse
                { Message = new ChatMessage(StaticValues.ChatMessageRoles.System, badJsonText) } }
                });

            var action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>().Where(e => e.ErrorCode == ErrorCode.FailedMenuDeserialization);
        }

        [Test]
        public async Task ShouldThrowFailedMenuGenerationError_WhenCompletionResultSuccessfulIsFalse()
        {
            _openAIService.Setup(x => x.ChatCompletion.CreateCompletion(It.IsAny<ChatCompletionCreateRequest>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ChatCompletionCreateResponse() { Error = new Error() });

            var action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>().Where(e => e.ErrorCode == ErrorCode.FailedMenuGeneration);
        }

        private void SetupContext()
        {
            _openAIService.Setup(x => x.ChatCompletion.CreateCompletion(It.IsAny<ChatCompletionCreateRequest>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ChatCompletionCreateResponse()
                {
                    Choices = new List<ChatChoiceResponse> { new ChatChoiceResponse
                { Message = new ChatMessage (StaticValues.ChatMessageRoles.System, goodJsonText ) } }
                });
        }

        private void CreateRequest()
        {
            _request = new GetGeneratedMenuQuery { MenuType = MenuType.FoodLover, PriceSuggestion = 50 };
        }

        [SetUp]
        public void Init()
        {
            _openAIService = new Mock<IOpenAIService>();
            _handler = new GetGeneratedMenuQueryHandler(_openAIService.Object);

            goodJsonText = @"{
            ""name"": ""Fitness Menu"",
            ""menuTypeId"": 1,
            ""meals"": [
                {
                    ""name"": ""Protein Power Breakfast"",
                    ""description"": ""Start your day with a protein-packed breakfast"",
                    ""price"": 10,
                    ""mealTypeId"": 1,
                    ""recipe"": {
                        ""name"": ""Protein Power Breakfast Recipe"",
                        ""description"": ""Fuel your morning with this nutritious and delicious breakfast recipe"",
                        ""ingredients"": [
                            ""Eggs"",
                            ""Spinach"",
                            ""Tomatoes"",
                            ""Avocado""
                        ]
                    }
                },
                {
                    ""name"": ""Lean High-Protein Lunch"",
                    ""description"": ""A satisfying lunch option packed with lean proteins"",
                    ""price"": 15,
                    ""mealTypeId"": 2,
                    ""recipe"": {
                        ""name"": ""Lean High-Protein Lunch Recipe"",
                        ""description"": ""Enjoy this balanced and protein-rich lunch recipe for sustained energy"",
                        ""ingredients"": [
                            ""Grilled chicken breast"",
                            ""Quinoa"",
                            ""Mixed vegetables""
                        ]
                    }
                },
                {
                    ""name"": ""Fit and Healthy Dinner"",
                    ""description"": ""Nourish your body with a wholesome dinner choice"",
                    ""price"": 20,
                    ""mealTypeId"": 3,
                    ""recipe"": {
                        ""name"": ""Fit and Healthy Dinner Recipe"",
                        ""description"": ""Treat yourself to this delicious and nutritious dinner recipe"",
                        ""ingredients"": [
                            ""Salmon fillet"",
                            ""Brown rice"",
                            ""Steamed broccoli""
                        ]
                    }
                },
                {
                    ""name"": ""Energy-Boosting Snack"",
                    ""description"": ""Refuel your body with an energizing snack"",
                    ""price"": 5,
                    ""mealTypeId"": 5,
                    ""recipe"": {
                        ""name"": ""Energy-Boosting Snack Recipe"",
                        ""description"": ""Satisfy your cravings with this tasty and nutritious snack recipe"",
                        ""ingredients"": [
                            ""Greek yogurt"",
                            ""Mixed berries"",
                            ""Almonds""
                        ]
                    }
                },
                {
                    ""name"": ""Protein-Packed Dessert"",
                    ""description"": ""Indulge in a guilt-free dessert with added protein"",
                    ""price"": 10,
                    ""mealTypeId"": 4,
                    ""recipe"": {
                        ""name"": ""Protein-Packed Dessert Recipe"",
                        ""description"": ""Treat yourself to this delicious and healthy dessert recipe"",
                        ""ingredients"": [
                            ""Chocolate protein powder"",
                            ""Banana"",
                            ""Almond milk""
                        ]
                    }
                }
              ]
            }";

            badJsonText = "{der;op}";
            CreateRequest();
            SetupContext();
        }
    }
}