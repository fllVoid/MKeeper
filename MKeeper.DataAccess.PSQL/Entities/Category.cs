namespace MKeeper.DataAccess.PSQL.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsIncoming { get; set; }
    public Category? ParentCategory { get; set; }
    public User User { get; set; } = null!;
}