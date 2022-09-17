using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface IScheduledTransactionRepository
{
    Task<ScheduledTransaction[]> Get(int[] accountIds, CancellationToken cancellationToken);

    Task<int> Add(ScheduledTransaction scheduledTransaction, CancellationToken cancellationToken);

    Task Update(ScheduledTransaction scheduledTransaction, CancellationToken cancellationToken);

    Task Delete(int scheduledTransactionId, CancellationToken cancellationToken);
}
