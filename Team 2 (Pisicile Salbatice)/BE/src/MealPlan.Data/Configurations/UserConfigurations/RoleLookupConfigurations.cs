using MealPlan.Data.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace MealPlan.Data.Configurations.UserConfigurations
{
    public class RoleLookupConfigurations : IEntityTypeConfiguration<RoleLookup>
    {
        public void Configure(EntityTypeBuilder<RoleLookup> builder)
        {
            builder
                .Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion<int>();

            builder
                .HasData(
                    Enum.GetValues(typeof(Role))
                        .Cast<Role>()
                        .Select(e => new RoleLookup()
                        {
                            Id = e,
                            Name = e.ToString()
                        })
                );
        }
    }
}
