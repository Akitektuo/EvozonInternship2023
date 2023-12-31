﻿using MealPlan.Business.Exceptions;
using MealPlan.Business.Menus.Models;
using MealPlan.Business.Menus.Queries;
using MediatR;
using OpenAI.Interfaces;
using OpenAI.ObjectModels.RequestModels;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OpenAIModels = OpenAI.ObjectModels.Models;

namespace MealPlan.Business.Menus.Handlers
{
    public class GetSuggestedMenuQueryHandler : IRequestHandler<GetSuggestedMenuQuery, SuggestedMenu>
    {
        private readonly IOpenAIService _openAIService;

        public GetSuggestedMenuQueryHandler(IOpenAIService openAIService)
        {
            _openAIService = openAIService;
        }

        public async Task<SuggestedMenu> Handle(GetSuggestedMenuQuery request, CancellationToken cancellationToken)
        {
            var completionResult = await _openAIService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
                {
                    ChatMessage.FromSystem(FromSystemPrompt()),
                    ChatMessage.FromUser($"Generate one menu from the {request.CategoryId} category with the suggested price of {request.PriceSuggestion}."),
                },
                Model = OpenAIModels.Gpt_3_5_Turbo,
                N = 1
            });

            if (!completionResult.Successful)
            {
                throw new CustomApplicationException(ErrorCode.UnsuccessfulMenuGeneration, "Menu could not be generated by ChatGPT");
            }

            var jsonMessage = completionResult.Choices.First().Message.Content;
            SuggestedMenu result;

            try
            {
                result = JsonSerializer.Deserialize<SuggestedMenu>(jsonMessage);
                if (result.Meals == null)
                    throw new JsonException();
            }
            catch (JsonException)
            {
                throw new CustomApplicationException(ErrorCode.UnsuccessfulMenuDeserialization, "Menu could not be deserialized");
            }

            result.TotalPrice = result.Meals.Sum(m => m.Price);
            return result;
        }

        private string FromSystemPrompt()
        {
			return "You are a menu generator for a restaurant.\r\n" +
				"You need to generate a json that can be translated in an object in C#.\r\n" +
				"The structure of the database is:\r\n" +
				"\r\n" +
				"public class SuggestedMenu\r\n" +
				"{\r\n" +
				"    public string Name { get; set; }\r\n" +
				"    public string Description { get; set; }\r\n" +
				"    public MenuCategory CategoryId { get; set; }\r\n" +
				"    public List<SuggestedMeal> Meals { get; set; }\r\n" +
				"}\r\n" +
				"\r\n" +
				"public enum MenuCategory\r\n" +
				"{\r\n" +
				"    Fitness = 1,\r\n" +
				"    FoodLover = 2,\r\n" +
				"    Gym = 3,\r\n" +
				"    Vegetarian = 4\r\n" +
				"}\r\n" +
				"\r\n" +
				"public class SuggestedMeal\r\n" +
				"{\r\n" +
				"    public string Name { get; set; }\r\n" +
				"    public double Price { get; set; }\r\n" +
				"    public MealType MealTypeId { get; set; }\r\n" +
				"    public SuggestedRecipe Recipe { get; set; }\r\n" +
				"}\r\n" +
				"\r\n" +
				"public enum MealType\r\n" +
				"{\r\n" +
				"    Breakfast = 1,\r\n" +
				"    Lunch = 2,\r\n" +
				"    Dinner = 3,\r\n" +
				"    Dessert = 4,\r\n" +
				"    Snack = 5\r\n" +
				"}\r\n" +
				"\r\n" +
				"public class SuggestedRecipe\r\n" +
				"{\r\n" +
				"    public string Description { get; set; }\r\n" +
				"    public string Name { get; set; }\r\n" +
				"    public List<SuggestedIngredient> Ingredients { get; set; }\r\n" +
				"}\r\n" +
				"\r\n" +
				"public class SuggestedIngredient\r\n" +
				"{\r\n" +
				"    public string Name { get; set; }\r\n" +
				"}\r\n" +
				"\r\n" +
				"The constraints are: you must have 5 meals, exactly one meal of each mealtype in a menu(Breakfast, Lunch, Dinner, Dessert, Snack), but you can generate whatever name for the meals you want.\r\n" +
				"The menu categories are:\r\n" +
				"Fitness: Energizing meals for active lifestyles\r\n" +
				"Food Lover: Indulgent dishes crafted for epicurean delight\r\n" +
				"Vegetarian: Wholesome plant-based creations bursting with flavor\r\n" +
				"Gym: Protein-rich nourishment to support rigorous training..\r\n" +
				"\r\n" +
				"For the ingredients, only return me a list of strings, so, instead of\r\n" +
				"{ingredients: [{name: eggs}, {name: cabbages}]}, you will give\r\n" +
				"{ingredients: [eggs, cabbages]}\r\n" +
				"\r\n" +
				"You will also be given a suggested price which represents the sum of all the meals. You have to reflect this price as follows:\r\n" +
				"If the price is lower than 300, you will generate cheap, common meals with cheap ingredients.\r\n" +
				"If the price is higher than 300 but smaller than 600, you will generate medium-priced meals.\r\n" +
				"If it is higher than 600, you will generate expensive meals with rare or expensive ingredients.\r\n" +
				"You will scale it accordingly, so, for example, a 350 price menu will have cheaper ingredients than a 500 one, taking into account that the maximum price is 1000, and that the sum of all meal prices must be equal to the suggested price\r\n" +
                "WRITE ONLY 1 LINE, NO WHITESPACE CHARACTERS, FOR JSON COMPRESSION";
        }
    }
}