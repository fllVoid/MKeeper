using MKeeper.Domain.Models;

namespace MKeeper.Domain.Services;

public interface ITransferService
{
    Task<int> Create(Transfer transfer);

    Task<Transfer[]> Get(int[] accountIds, DateTime from, DateTime to);

    Task<Transfer[]> Get(int[] accountIds);

    Task Update(Transfer transfer);

    Task Delete(int transferId);
}
