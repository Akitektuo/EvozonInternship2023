using System.Linq;

namespace MealPlan.Data
{
    public static class DataExtensions
    {
        public static IQueryable<Thing> Pagination<Thing>(this IQueryable<Thing> query, int pageNumber, int pageSize)
        {
            return query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
