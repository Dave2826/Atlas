namespace Atlas.Models.Entities;

public class Sale
{
    public int SaleId { get; set; }

    public int? CustomerId { get; set; }

    public int UserId { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties

    public Customer? Customer { get; set; }

    public User User { get; set; } = null!;

    public ICollection<SaleDetail> SaleDetails { get; set; }
        = new List<SaleDetail>();

    public ICollection<SalePayment> SalePayments { get; set; }
        = new List<SalePayment>();
}