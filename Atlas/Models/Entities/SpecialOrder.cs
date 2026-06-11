using Atlas.Models.Enums;

namespace Atlas.Models.Entities;

public class SpecialOrder
{
    public int SpecialOrderId { get; set; }

    public int? CustomerId { get; set; }

    public int? LayawayItemId { get; set; }

    public SpecialOrderType OrderType { get; set; }

    public string RequestedItem { get; set; } = string.Empty;

    public string? Notes { get; set; }

    public SpecialOrderStatus Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties

    public Customer? Customer { get; set; }

    public LayawayItem? LayawayItem { get; set; }
}