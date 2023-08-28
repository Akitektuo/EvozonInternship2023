using MealPlan.Data.Models.Recipes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealPlan.Data.Configurations.RecipeConfigurations
{
    public class RecipeConfigurations : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);

            builder.HasMany(e => e.Ingredients).WithMany(e => e.Recipes).UsingEntity<RecipeIngredient>();
            
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
