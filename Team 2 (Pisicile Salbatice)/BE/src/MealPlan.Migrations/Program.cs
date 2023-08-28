using Microsoft.EntityFrameworkCore;

namespace MealPlan.Migrations
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var context = new DesignTimeContextFactory().CreateDbContext(args);
            context.Database.Migrate();
        }
    }
}
