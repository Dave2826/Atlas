namespace Atlas.Models.Entities;

public class Size
{
    public int SizeId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<ProductTypeSize> ProductTypeSizes { get; set; }
        = new List<ProductTypeSize>();
}
