using Atlas.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Data.Configurations;

public class ProductSizeStockConfiguration : IEntityTypeConfiguration<ProductSizeStock>
{
    public void Configure(EntityTypeBuilder<ProductSizeStock> builder)
    {
        builder.HasKey(pss => pss.ProductSizeStockId);

        builder.HasOne(pss => pss.Product)
            .WithMany(p => p.ProductSizeStocks)
            .HasForeignKey(pss => pss.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}