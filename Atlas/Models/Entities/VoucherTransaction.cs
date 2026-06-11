namespace Atlas.Models.Entities;

public class VoucherTransaction
{
    public int VoucherTransactionId { get; set; }

    public int VoucherId { get; set; }

    public decimal Amount { get; set; }

    public string TransactionType { get; set; } = string.Empty;

    public int? ReferenceId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Property

    public Voucher Voucher { get; set; } = null!;
}