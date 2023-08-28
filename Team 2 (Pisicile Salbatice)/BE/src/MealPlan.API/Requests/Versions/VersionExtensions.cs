using MealPlan.Business.Versions.Queries;

namespace MealPlan.API.Requests.Versions
{
    public static class VersionExtensions
    {
        public static GetVersionQuery ToQuery(this GetVersionRequest request)
        {
            return new GetVersionQuery
            {
                Name = request.Name
            };
        }
    }
}
