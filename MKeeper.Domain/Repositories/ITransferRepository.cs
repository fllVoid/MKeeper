using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface ITransferRepository
{
    Task<Transfer[]> Get(int[] accountIds, DateTime from, DateTime to, CancellationToken cancellationToken);

    Task<Transfer[]> Get(int[] accountIds, CancellationToken cancellationToken);

    Task<int> Add(Transfer transfer, CancellationToken cancellationToken);

    Task Update(Transfer transfer, CancellationToken cancellationToken);

    Task Delete(int transferId, CancellationToken cancellationToken);
}
