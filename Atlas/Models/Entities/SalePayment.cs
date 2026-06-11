using Atlas.Models.Enums;

namespace Atlas.Models.Entities;

public class SalePayment
{
    public int SalePaymentId { get; set; }

    public int SaleId { get; set; }

    public PaymentMethod PaymentMethod { get; set; }

    public decimal Amount { get; set; }

    public int? VoucherId { get; set; }

    // Navigation Properties

    public Sale Sale { get; set; } = null!;

    public Voucher? Voucher { get; set; }
}