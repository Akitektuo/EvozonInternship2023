using MealPlan.Data;
using Microsoft.Extensions.DependencyInjection;

namespace MealPlan.API.DependencyRegistration
{
    public static class Services
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<MealPlanContext, MealPlanContext>();
        }
    }
}
