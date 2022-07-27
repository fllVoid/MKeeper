using Microsoft.EntityFrameworkCore;
using MKeeper.Domain.Models;
using MKeeper.Domain.Models.Abstract;

namespace MKeeper.DataAccess.PSQL;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<BaseTransaction> baseTransactions { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;
    public DbSet<Transfer> Transfers { get; set; } = null!;
    public DbSet<Debt> Debts { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Currency> Currencies { get; set; } = null!;
    public DbSet<ScheduledTransaction> ScheduledTransactions { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
