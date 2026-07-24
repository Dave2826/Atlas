namespace Atlas.Models.Entities;

public class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? InternalCode { get; set; }

    public string? ImageUrl { get; set; }

    public int BrandId { get; set; }

    public int ProductTypeId { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public Brand? Brand { get; set; }

    public ProductType? ProductType { get; set; }

    public ICollection<ProductSizeStock> ProductSizeStocks { get; set; }
        = new List<ProductSizeStock>();
}
