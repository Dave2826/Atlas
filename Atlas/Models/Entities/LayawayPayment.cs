using Atlas.Models.Enums;

namespace Atlas.Models.Entities;

public class LayawayPayment
{
    public int LayawayPaymentId { get; set; }

    public int LayawayId { get; set; }

    public int? LayawayItemId { get; set; }

    public decimal Amount { get; set; }

    public PaymentMethod PaymentMethod { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string? Notes { get; set; }

    // Navigation Properties

    public Layaway Layaway { get; set; } = null!;

    public LayawayItem? LayawayItem { get; set; }
}