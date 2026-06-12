using Atlas.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.CustomerId);

        builder.Property(c => c.FirstName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(c => c.LastName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(c => c.Phone)
            .HasMaxLength(50);

        // Prevent cascade deletes from Customer -> Layaways and Vouchers
        
    }
}
