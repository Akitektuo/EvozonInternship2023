using MealPlan.Data.Models.Meals;
using Microsoft.EntityFrameworkCore;

namespace MealPlan.Data.Configurations.MealConfigurations
{
    public class MenuConfigurations : IEntityTypeConfiguration<Menu>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Menu> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
