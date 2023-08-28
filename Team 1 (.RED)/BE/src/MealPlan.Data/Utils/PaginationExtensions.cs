using System.Linq;

namespace MealPlan.Data.Utils
{
    public static class PaginationExtensions
    {
        public static IQueryable<T> ToPage<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        } 
    }
}