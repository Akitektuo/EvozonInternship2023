using MealPlan.Data.Models.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace MealPlan.Data.Configurations.Orders
{
    public class OrderStatusLookupConfiguration : IEntityTypeConfiguration<OrderStatusLookup>
    {
        public void Configure(EntityTypeBuilder<OrderStatusLookup> builder)
        {
            builder.Property(x => x.Id)
               .ValueGeneratedNever()
               .HasConversion<int>();

            builder.HasData(
                Enum.GetValues(typeof(OrderStatus))
                .Cast<OrderStatus>()
                .Select(e => new OrderStatusLookup()
                {
                    Id = e,
                    Name = e.ToString()
                }));
        }
    }
}