using System;
using System.Text.Json;

namespace MealPlan.Business.Exceptions
{
    public class CustomApplicationException : Exception
    {
        public ErrorCode ErrorCode { get; }
        public CustomApplicationException(ErrorCode code, string message) :
            base(JsonSerializer.Serialize(
                new CustomException
                {
                    Code = (int)code,
                    Message = message.ToString()
                },
                new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }))
        {
            ErrorCode = code;
        }
    }
}
