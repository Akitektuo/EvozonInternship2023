using MealPlan.Data.Models.Meals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace MealPlan.Data.Configurations.MealConfigurations
{
    public class MenuTypeLookupConfigurations : IEntityTypeConfiguration<MenuTypeLookup>
    {
        public void Configure(EntityTypeBuilder<MenuTypeLookup> builder)
        {
            builder
                .Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion<int>();

            builder
                .HasData(
                    Enum.GetValues(typeof(MenuType))
                        .Cast<MenuType>()
                        .Select(e => new MenuTypeLookup()
                        {
                            Id = e,
                            Name = e.ToString()
                        })
                );
        }
    }
}
