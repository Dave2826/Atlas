using Atlas.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.UserId);

        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(u => u.IsActive)
            .HasDefaultValue(true);

        // Seed data
        builder.HasData(
            new User 
            { 
                UserId = 1, 
                Username = "admin", 
                PasswordHash = "$2a$12$RjkXAdYI5.5Jk8HmVFvQ.OO9L0jZ7HfD5GfQqHd5Xr2KvJ8hYbN2e", 
                RoleId = 1,
                IsActive = true,
                CreatedAt = new DateTime(2026, 6, 15, 0, 0, 0, DateTimeKind.Utc),
                LastLoginAt = null
            }
        );

        // Relationship: User (many) -> Role (one)
        builder.HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
