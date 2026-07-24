namespace Atlas.Models.Entities;

public class ProductTypeSize
{
    public int ProductTypeId { get; set; }

    public int SizeId { get; set; }

    public ProductType ProductType { get; set; } = null!;

    public Size Size { get; set; } = null!;
}
