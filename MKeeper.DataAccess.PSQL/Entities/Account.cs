namespace MKeeper.DataAccess.PSQL.Entities;

public class Account
{
    public int Id { get; set; }
    public decimal Balance { get; set; }
    public Currency Currency { get; set; } = null!;
    public User User { get; set; } = null!;
}
