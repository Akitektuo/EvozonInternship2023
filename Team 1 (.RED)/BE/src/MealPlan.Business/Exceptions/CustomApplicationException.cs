using System;
using System.Text.Json;

namespace MealPlan.Business.Exceptions
{
    public class CustomApplicationException : Exception
    {
        public ErrorCode ErrorCode { get; set; }

        public CustomApplicationException(ErrorCode errorCode, string message) : 
            base(JsonSerializer.Serialize(
                new CustomException
                {
                    Message = message,
                    Code = (int)errorCode
                },
                
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
                ))
        {
            ErrorCode = errorCode;
        }
    }
}
