using Atlas.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.ProductId);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.BrandName)
            .HasMaxLength(200);
            
        builder.Property(p => p.SKU)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.HasIndex(p => p.SKU)
            .IsUnique();

        builder.Property(p => p.Color)
            .HasMaxLength(30);

        builder.Property(p => p.Description)
            .HasMaxLength(1000);

        // Prevent cascade delete from Product -> ProductSizeStock
        
    }
}
