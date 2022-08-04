using MKeeper.Domain.Models.Abstract;

namespace MKeeper.DataAccess.PSQL.Entities;

public class Debt : BaseTransaction
{
    public DateTime RepaymentDate { get; set; }
    public string SubjectName { get; set; } = null!;
}

