namespace Atlas.Models.Entities;

public class SaleDetail
{
    public int SaleDetailId { get; set; }

    public int SaleId { get; set; }

    public int ProductSizeStockId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Subtotal { get; set; }

    // Navigation Properties

    public Sale Sale { get; set; } = null!;

    public ProductSizeStock ProductSizeStock { get; set; } = null!;
}