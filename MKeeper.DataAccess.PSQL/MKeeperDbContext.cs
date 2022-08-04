using Microsoft.EntityFrameworkCore;
using MKeeper.DataAccess.PSQL.Entities;
using MKeeper.DataAccess.PSQL.Entities.Abstract;

namespace MKeeper.DataAccess.PSQL;

public class MKeeperDbContext : DbContext
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

    public MKeeperDbContext(DbContextOptions<MKeeperDbContext> options)
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
