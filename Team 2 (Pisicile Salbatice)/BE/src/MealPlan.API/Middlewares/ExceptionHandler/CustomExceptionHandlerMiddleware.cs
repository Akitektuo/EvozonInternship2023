using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using MealPlan.Business.Exceptions;
using System.Net;

namespace MealPlan.API.Middlewares.ExceptionHandler
{
    public class CustomExceptionHandlerMiddleware
    {

        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomApplicationException exception)
            {
                await BuildResponse(context, exception.Message, StatusCodes.Status409Conflict);
            }
            catch (Exception exception)
            {
                await BuildResponse(context, exception.Message, StatusCodes.Status500InternalServerError);
            }
        }

        private static async Task BuildResponse(HttpContext context, string message, int code)
        {
            context.Response.Clear();
            context.Response.StatusCode = code;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(message);
        }
    }
}
