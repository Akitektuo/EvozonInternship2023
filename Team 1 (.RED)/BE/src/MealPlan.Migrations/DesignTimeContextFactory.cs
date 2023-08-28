using MealPlan.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Linq;

namespace MealPlan.Migrations
{
    public class DesignTimeContextFactory : IDesignTimeDbContextFactory<MealPlanContext>
    {
        private const string LocalSql = "server=localhost;database=MealPlan-Local;Trusted_Connection=True;";

        private static readonly string MigrationAssemblyName = typeof(DesignTimeContextFactory).Assembly.GetName().Name;

        public MealPlanContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MealPlanContext>()
                .UseSqlServer(args.FirstOrDefault() ?? LocalSql,
                op => op.MigrationsAssembly(MigrationAssemblyName));
            return new MealPlanContext(builder.Options);
        }
    }
}
