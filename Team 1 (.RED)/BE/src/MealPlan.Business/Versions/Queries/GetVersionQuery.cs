using MealPlan.Business.Versions.Models;
using MediatR;

namespace MealPlan.Business.Versions.Queries
{
    public class GetVersionQuery : IRequest<VersionCode>
    {
        public string Name { get; set; }
    }
}
