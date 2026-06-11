using Atlas.Models.Enums;

namespace Atlas.Models.Entities;

public class Voucher
{
    public int VoucherId { get; set; }

    public int CustomerId { get; set; }

    public decimal OriginalAmount { get; set; }

    public decimal RemainingAmount { get; set; }

    public DateTime ExpirationDate { get; set; }

    public VoucherStatus Status { get; set; }

    public VoucherSourceType SourceType { get; set; }

    public string? SourceReference { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties

    public Customer Customer { get; set; } = null!;

    public ICollection<VoucherTransaction> VoucherTransactions { get; set; }
        = new List<VoucherTransaction>();
}