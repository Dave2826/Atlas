namespace Atlas.Models.Entities;

public class CashSession
{
    public int CashSessionId { get; set; }

    public int UserId { get; set; }

    public decimal OpeningAmount { get; set; }

    public decimal ClosingAmount { get; set; }

    public DateTime OpenedAt { get; set; }

    public DateTime? ClosedAt { get; set; }

    // Navigation Properties

    public User User { get; set; } = null!;

    public ICollection<Expense> Expenses { get; set; }
        = new List<Expense>();
}