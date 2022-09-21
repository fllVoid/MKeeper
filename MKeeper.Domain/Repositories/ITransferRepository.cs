using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface ITransferRepository
{
    Task<Transfer[]> Get(int[] accountIds, DateTime from, DateTime to, CancellationToken cancellationToken = default);

    Task<Transfer[]> Get(int[] accountIds, CancellationToken cancellationToken = default);

    Task<int> Add(Transfer transfer, CancellationToken cancellationToken = default);

    Task Update(Transfer transfer, CancellationToken cancellationToken = default);

    Task Delete(int transferId, CancellationToken cancellationToken = default);
}
