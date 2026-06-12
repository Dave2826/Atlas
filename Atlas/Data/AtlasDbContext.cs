using Atlas.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Data;

public class AtlasDbContext : DbContext
{
    public AtlasDbContext(DbContextOptions<AtlasDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Register all IEntityTypeConfiguration implementations in this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AtlasDbContext).Assembly);
    }

    public DbSet<Department> Departments => Set<Department>();

    public DbSet<ProductType> ProductTypes => Set<ProductType>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<ProductSizeStock> ProductSizeStocks => Set<ProductSizeStock>();

    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Layaway> Layaways => Set<Layaway>();

    public DbSet<LayawayItem> LayawayItems => Set<LayawayItem>();

    public DbSet<LayawayPayment> LayawayPayments => Set<LayawayPayment>();

    public DbSet<Voucher> Vouchers => Set<Voucher>();

    public DbSet<VoucherTransaction> VoucherTransactions => Set<VoucherTransaction>();

    public DbSet<Sale> Sales => Set<Sale>();

    public DbSet<SaleDetail> SaleDetails => Set<SaleDetail>();

    public DbSet<SalePayment> SalePayments => Set<SalePayment>();

    public DbSet<SpecialOrder> SpecialOrders => Set<SpecialOrder>();

    public DbSet<Role> Roles => Set<Role>();

    public DbSet<User> Users => Set<User>();

    public DbSet<CashSession> CashSessions => Set<CashSession>();

    public DbSet<Expense> Expenses => Set<Expense>();
}
