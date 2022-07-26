using MKeeper.Domain.Models.Abstract;

namespace MKeeper.Domain.Models;

public class Debt : BaseTransaction
{
    public DateTime RepaymentDate { get; set; }
    public string SubjectName { get; set; } = null!;
}

