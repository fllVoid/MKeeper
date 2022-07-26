using MKeeper.Domain.Models.Abstract;

namespace MKeeper.Domain.Models;

public class Transaction : BaseTransaction
{
    public Category Category { get; set; } = null!;
}
