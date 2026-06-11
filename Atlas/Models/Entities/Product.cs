namespace Atlas.Models.Entities;
using Atlas.Models.Enums;

public class Product
{
    public int ProductId { get; set; }

    public int DepartmentId { get; set; }

    public int ProductTypeId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? BrandName { get; set; }

    public string? Description { get; set; }

    public decimal BaseSalePrice { get; set; }

    public decimal CostPrice { get; set; }

    public ProductStatus Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties

    public Department Department { get; set; } = null!;

    public ProductType ProductType { get; set; } = null!;

    public ICollection<ProductSizeStock> ProductSizeStocks { get; set; }
        = new List<ProductSizeStock>();
}