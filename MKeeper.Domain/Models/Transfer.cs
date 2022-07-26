using MKeeper.Domain.Models.Abstract;

namespace MKeeper.Domain.Models;

public class Transfer : BaseTransaction
{
    public Account? DestinationAccount { get; set; }
}
