using MKeeper.Domain.Models.Abstract;

namespace MKeeper.DataAccess.PSQL.Entities;

public class ScheduledTransaction : BaseTransaction
{
    public DateTime InitialDate { get; set; }
    public Intervals Interval { get; set; }
    public Category Category { get; set; } = null!;
    public Account Account { get; set; } = null!;
}
