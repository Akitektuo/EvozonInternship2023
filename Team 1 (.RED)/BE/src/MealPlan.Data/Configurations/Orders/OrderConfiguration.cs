using MealPlan.Data.Models.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealPlan.Data.Configurations.Orders
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Address).IsRequired().HasMaxLength(70);
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.MenuId).IsRequired();
            builder.Property(x => x.OrderStatusId).IsRequired().HasDefaultValue(OrderStatus.Pending).HasConversion<int>();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            builder.HasOne(x => x.Menu)
               .WithMany()
               .HasForeignKey(x => x.MenuId)
               .IsRequired();
        }
    }
}