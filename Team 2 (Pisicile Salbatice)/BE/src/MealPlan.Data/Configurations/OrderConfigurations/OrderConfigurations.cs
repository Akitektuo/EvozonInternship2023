using MealPlan.Data.Models.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealPlan.Data.Configurations.OrderConfigurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.TotalPrice)
                .IsRequired();
            builder.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.ShippingAddress)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.MenuName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.UserEmail)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.StartDate)
                .IsRequired();
            builder.Property(x => x.EndDate)
                .IsRequired();
            builder.Property(x => x.StatusId)
                .HasDefaultValue(Status.WaitingForApproval);
        }
    }
}
