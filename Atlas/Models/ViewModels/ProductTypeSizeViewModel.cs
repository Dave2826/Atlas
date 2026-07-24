namespace Atlas.Models.ViewModels;

public class ProductTypeSizeViewModel
{
    public int ProductTypeId { get; set; }

    public string ProductTypeName { get; set; } = string.Empty;

    public List<SizeCheckItem> Sizes { get; set; } = new();
}

public class SizeCheckItem
{
    public int SizeId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }

    public bool IsSelected { get; set; }
}
