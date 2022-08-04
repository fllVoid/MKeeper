using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface ITransferRepository
{
    Task<Transfer[]> Get(int[] accountIds, DateTime from, DateTime to);

    Task<Transfer[]> Get(int[] accountIds);

    Task<int> Add(Transfer Transfer);

    Task Update(Transfer Transfer);

    Task Delete(int TransferId);
}
