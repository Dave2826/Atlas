namespace Atlas.Models.Entities;

public class Expense
{
    public int ExpenseId { get; set; }

    public int CashSessionId { get; set; }

    public string Description { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Property

    public CashSession CashSession { get; set; } = null!;
}