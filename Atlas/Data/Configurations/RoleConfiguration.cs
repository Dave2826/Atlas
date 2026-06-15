using Atlas.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.RoleId);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100);

        // Seed data
        builder.HasData(
            new Role
            {
                RoleId = 1,
                Name = "Administrator"
            },
            new Role
            {
                RoleId = 2,
                Name = "Employee"
            }
        );

        // Prevent cascade delete from Role -> Users
        
    }
}
