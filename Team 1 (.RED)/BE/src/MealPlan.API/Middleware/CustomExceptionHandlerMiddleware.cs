using MealPlan.Business.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MealPlan.API.Middleware
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
                await BuildResponse(context, exception.Message, HttpStatusCode.Conflict);
            }
            catch (Exception exception)
            {
                await BuildResponse(context, exception.Message, HttpStatusCode.InternalServerError);
            }
        }

        private static async Task BuildResponse(HttpContext context, string message, HttpStatusCode statusCode)
        {
            var response = context.Response;

            response.Clear();
            response.StatusCode = (int)statusCode;
            response.ContentType = "application/json";

            await context.Response.WriteAsync(message);
        }
    }
}