using Atlas.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Data.Configurations;

public class ProductTypeSizeConfiguration : IEntityTypeConfiguration<ProductTypeSize>
{
    public void Configure(EntityTypeBuilder<ProductTypeSize> builder)
    {
        builder.HasKey(pts => new { pts.ProductTypeId, pts.SizeId });

        builder.HasOne(pts => pts.ProductType)
            .WithMany(pt => pt.ProductTypeSizes)
            .HasForeignKey(pts => pts.ProductTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pts => pts.Size)
            .WithMany(s => s.ProductTypeSizes)
            .HasForeignKey(pts => pts.SizeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
