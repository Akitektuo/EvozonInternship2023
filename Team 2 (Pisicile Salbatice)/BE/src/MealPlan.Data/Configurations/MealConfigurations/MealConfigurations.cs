using MealPlan.Data.Models.Meals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealPlan.Data.Configurations.MealConfigurations
{
    public class MealConfigurations : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(1024);
            builder.Property(x => x.Price).IsRequired();

            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
