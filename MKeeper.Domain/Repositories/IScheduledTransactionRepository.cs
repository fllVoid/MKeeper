using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface IScheduledTransactionRepository
{
    Task<ScheduledTransaction[]> Get(int[] accountIds);

    Task<int> Add(ScheduledTransaction scheduledTransaction);

    Task Update(ScheduledTransaction scheduledTransaction);

    Task Delete(int sheduledTransactionId);
}
