using MealPlan.Data.Models.Recipes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealPlan.Data.Configurations.Recipes
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(40);

            builder.HasMany(e => e.Recipes)
                .WithMany(e => e.Ingredients)
                .UsingEntity<RecipeIngredient>();
        }
    }
}