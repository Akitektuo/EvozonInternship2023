using MealPlan.Business.Exceptions;
using MealPlan.Business.Menus.Models;
using MealPlan.Business.Menus.Queries;
using MealPlan.Business.Shared;
using MediatR;
using OpenAI.Interfaces;
using OpenAI.ObjectModels.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OpenAiModels = OpenAI.ObjectModels.Models;

namespace MealPlan.Business.Menus.Handlers
{
    public class GetGeneratedMenuQueryHandler : IRequestHandler<GetGeneratedMenuQuery, GeneratedMenuModel>
    {
        private readonly IOpenAIService _openAIService;

        public GetGeneratedMenuQueryHandler(IOpenAIService openAIService)
        {
            _openAIService = openAIService;
        }

        public async Task<GeneratedMenuModel> Handle(GetGeneratedMenuQuery request, CancellationToken cancellationToken)
        {
            var completionResult = await _openAIService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
                {
                    ChatMessage.FromSystem(OpenAiConstants.GenerateMenuPrompt),
                    ChatMessage.FromUser($"I would like a menu from the category: {request.MenuType} with the price suggestion of {request.PriceSuggestion} dollars")
                },
                Model = OpenAiModels.Gpt_3_5_Turbo,
                MaxTokens = 2048,
                N = 1,
                PresencePenalty = 2
            });

            if(completionResult.Successful == false)
            {
                throw new CustomApplicationException(ErrorCode.FailedMenuGeneration, "Failed menu generation using OpenAI.");
            }
            try
            {
                var result = JsonSerializer.Deserialize<GeneratedMenuModel>(completionResult.Choices.First().Message.Content,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return result;
            }
            catch(Exception)
            {
                throw new CustomApplicationException(ErrorCode.FailedMenuDeserialization, "Failed deserialization of generated menu.");
            }
        }
    }
}