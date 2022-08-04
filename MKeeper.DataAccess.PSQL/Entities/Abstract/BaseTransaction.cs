namespace MKeeper.DataAccess.PSQL.Entities.Abstract;

public abstract class BaseTransaction
{
    public int Id { get; set; }
    public decimal Sum { get; set; }
    public DateTime CreationDate { get; set; }
    public Account SourceAccount { get; set; } = null!;
    public string? Comment { get; set; }
}
