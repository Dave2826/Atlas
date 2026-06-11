using static System.Environment;

namespace Atlas.Models.Entities;

public class Customer
{
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string? Phone { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties

    public ICollection<Layaway> Layaways { get; set; }
        = new List<Layaway>();

    public ICollection<Voucher> Vouchers { get; set; }
        = new List<Voucher>();

    public ICollection<Sale> Sales { get; set; }
        = new List<Sale>();

    public ICollection<SpecialOrder> SpecialOrders { get; set; }
        = new List<SpecialOrder>();
}