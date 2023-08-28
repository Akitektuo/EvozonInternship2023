using MealPlan.API.Authorization;
using MealPlan.API.Services.Authentication;
using MealPlan.API.Services.Authorization;
using MealPlan.API.Services.UserIdentity;
using MealPlan.Business.Services;
using MealPlan.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using IAuthorizationService = MealPlan.API.Services.Authorization.IAuthorizationService;

namespace MealPlan.API.DependencyRegistration
{
    public static class Services
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<MealPlanContext, MealPlanContext>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthorizationHandler, CustomAuthorizationHandler>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IUserIdentityService, UserIdentityService>();

            services.AddScoped<IOrderStatusService, OrderStatusService>();
        }
    }
}
