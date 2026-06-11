using Atlas.Models.Enums;

namespace Atlas.Models.Entities;

public class Layaway
{
    public int LayawayId { get; set; }

    public int CustomerId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime ExpirationDate { get; set; }

    public LayawayStatus Status { get; set; }

    public string? Notes { get; set; }

    // Navigation Properties

    public Customer Customer { get; set; } = null!;

    public ICollection<LayawayItem> LayawayItems { get; set; }
        = new List<LayawayItem>();

    public ICollection<LayawayPayment> LayawayPayments { get; set; }
        = new List<LayawayPayment>();
}