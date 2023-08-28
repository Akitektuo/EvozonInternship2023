using MealPlan.Data.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealPlan.Data.Configurations.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(20).UseCollation("Latin1_General_CS_AS");
            builder.Property(x => x.RoleId).IsRequired().HasDefaultValue(Role.User).HasConversion<int>();
            builder.Property(x => x.WalletBalance).IsRequired().HasDefaultValue(0);

            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}