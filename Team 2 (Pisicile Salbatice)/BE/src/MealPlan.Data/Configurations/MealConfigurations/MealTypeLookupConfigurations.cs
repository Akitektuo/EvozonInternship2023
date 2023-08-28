using MealPlan.Data.Models.Meals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace MealPlan.Data.Configurations.MealConfigurations
{
    public class MealTypeLookupConfigurations : IEntityTypeConfiguration<MealTypeLookup>
    {
        public void Configure(EntityTypeBuilder<MealTypeLookup> builder)
        {
            builder
                .Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion<int>();

            builder
                .HasData(
                    Enum.GetValues(typeof(MealType))
                        .Cast<MealType>()
                        .Select(e => new MealTypeLookup()
                        {
                            Id = e,
                            Name = e.ToString()
                        })
                );
        }
    }
}
