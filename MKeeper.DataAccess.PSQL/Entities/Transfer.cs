using MKeeper.Domain.Models.Abstract;

namespace MKeeper.DataAccess.PSQL.Entities;

public class Transfer : BaseTransaction
{
    public Account DestinationAccount { get; set; } = null!;
}
