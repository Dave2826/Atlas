using Atlas.Models.Enums;
using static System.Environment;

namespace Atlas.Models.Entities;

public class LayawayItem
{
    public int LayawayItemId { get; set; }

    public int LayawayId { get; set; }

    public int ProductSizeStockId { get; set; }

    public int Quantity { get; set; }

    public decimal OriginalPrice { get; set; }

    public decimal PaidAmount { get; set; }

    public LayawayItemStatus Status { get; set; }

    // Navigation Properties

    public Layaway Layaway { get; set; } = null!;

    public ProductSizeStock ProductSizeStock { get; set; } = null!;

    public ICollection<LayawayPayment> LayawayPayments { get; set; }
        = new List<LayawayPayment>();

    public ICollection<SpecialOrder> SpecialOrders { get; set; }
        = new List<SpecialOrder>();
}