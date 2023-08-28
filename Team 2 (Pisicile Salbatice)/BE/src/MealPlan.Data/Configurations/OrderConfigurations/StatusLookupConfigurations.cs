using MealPlan.Data.Models.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace MealPlan.Data.Configurations.OrderConfigurations
{
    public class StatusLookupConfigurations : IEntityTypeConfiguration<StatusLookup>
    {
        public void Configure(EntityTypeBuilder<StatusLookup> builder)
        {
            builder
                .Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion<int>();

            builder
                .HasData(
                    Enum.GetValues(typeof(Status))
                        .Cast<Status>()
                        .Select(e => new StatusLookup()
                        {
                            Id = e,
                            Name = e.ToString()
                        })
                );
        }
    }
}
