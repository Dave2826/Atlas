using Atlas.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Data.Configurations;

public class LayawayConfiguration : IEntityTypeConfiguration<Layaway>
{
    public void Configure(EntityTypeBuilder<Layaway> builder)
    {
        builder.HasKey(l => l.LayawayId);

        builder.HasOne(l => l.Customer)
            .WithMany(c => c.Layaways)
            .HasForeignKey(l => l.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}