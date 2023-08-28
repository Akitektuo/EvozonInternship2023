using MealPlan.Data.Models.Recipes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealPlan.Data.Configurations.RecipeConfigurations
{
    public class IngredientConfigurations : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
