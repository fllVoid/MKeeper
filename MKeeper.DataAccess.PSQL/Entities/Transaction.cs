using MKeeper.DataAccess.PSQL.Entities.Abstract;

namespace MKeeper.DataAccess.PSQL.Entities;

public class Transaction : BaseTransaction
{
    public Category Category { get; set; } = null!;
}
