using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface IScheduledTransactionRepository
{
    Task<ScheduledTransaction[]> Get(int[] accountIds, CancellationToken cancellationToken = default);

    Task<int> Add(ScheduledTransaction scheduledTransaction, CancellationToken cancellationToken = default);

    Task Update(ScheduledTransaction scheduledTransaction, CancellationToken cancellationToken = default);

    Task Delete(int scheduledTransactionId, CancellationToken cancellationToken = default);
}
