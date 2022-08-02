using MKeeper.Domain.Models;
using MKeeper.Domain.Models.Abstract;

namespace MKeeper.Domain.Repositories;

public interface IAbstractTransactionRepository<T> where T : BaseTransaction
{
    Task<T[]> Get(
        int[] accountIds,
        int[] categoryIds,
        DateTime from,
        DateTime to);

    Task<T[]> Get(int accountId,
        int[] categoryIds,
        DateTime from,
        DateTime to);

    Task<T[]> Get(int accountId, int[] categoryIds);

    Task<T[]> Get(int accountId);

    Task<T[]> Get(int[] accountIds, int[] categoryIds);

    Task<T[]> Get(int[] accountIds);

    Task<T[]> GetAll();

    Task<int> Add(T transaction);

    Task Update(T transaction);

    Task Delete(int transactionId);
}
