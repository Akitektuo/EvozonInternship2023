using MealPlan.Data.Models.Meals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealPlan.Data.Configurations.Meals
{
    public class MealConfiguration : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        }
    }
}