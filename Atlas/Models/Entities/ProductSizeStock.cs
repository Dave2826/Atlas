using Atlas.Models.Enums;

namespace Atlas.Models.Entities;

public class ProductSizeStock
{
    public int ProductSizeStockId { get; set; }

    public int ProductId { get; set; }

    public string Size { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal PriceAdjustment { get; set; }

    public ProductSizeStockStatus Status { get; set; }

    // Navigation Properties

    public Product Product { get; set; } = null!;
}