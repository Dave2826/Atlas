using Atlas.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Data.Configurations;

public class SizeConfiguration : IEntityTypeConfiguration<Size>
{
    public void Configure(EntityTypeBuilder<Size> builder)
    {
        builder.ToTable("Sizes");

        builder.HasKey(s => s.SizeId);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.Property(s => s.Description)
            .HasColumnType("varchar(250)");

        builder.Property(s => s.DisplayOrder)
            .HasDefaultValue(0);

        builder.Property(s => s.IsActive)
            .HasDefaultValue(true);
    }
}
