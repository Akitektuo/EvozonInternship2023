using MealPlan.Data.Models.Recipes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealPlan.Data.Configurations.RecipeConfigurations
{
    public class RecipeIngredientConfigurations : IEntityTypeConfiguration<RecipeIngredient>
    {
        public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
        {
            builder.HasIndex(p => new { p.RecipeId, p.IngredientId }).IsUnique();
        }
    }
}
