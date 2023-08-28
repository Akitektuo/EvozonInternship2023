using MealPlan.Data.Models.Menus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace MealPlan.Data.Configurations.Menus
{
    public class MenuCategoryLookupConfiguration : IEntityTypeConfiguration<MenuCategoryLookup>
    {
        public void Configure(EntityTypeBuilder<MenuCategoryLookup> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion<int>();

            builder.HasData(
                Enum.GetValues(typeof(MenuCategory))
                .Cast<MenuCategory>()
                .Select(e => new MenuCategoryLookup()
                {
                    Id = e,
                    Name = e.ToString()
                }));
        }
    }
}