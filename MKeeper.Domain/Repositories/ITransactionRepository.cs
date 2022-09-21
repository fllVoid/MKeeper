using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface ITransactionRepository
{
    Task<Transaction[]> Get(int[] accountIds, int[] categoryIds, DateTime from, DateTime to, CancellationToken cancellationToken = default);

    //Task<Transaction[]> Get(int accountId, int[] categoryIds, DateTime from, DateTime to);

    //Task<Transaction[]> Get(int accountId, int[] categoryIds);

    //Task<Transaction[]> Get(int accountId);

    Task<Transaction[]> Get(int[] accountIds, int[] categoryIds, CancellationToken cancellationToken = default);

    Task<Transaction[]> Get(int[] accountIds, CancellationToken cancellationToken = default);

    Task<int> Add(Transaction transaction, CancellationToken cancellationToken = default);

    Task Update(Transaction transaction, CancellationToken cancellationToken = default);

    Task Delete(int transactionId, CancellationToken cancellationToken = default);
}
