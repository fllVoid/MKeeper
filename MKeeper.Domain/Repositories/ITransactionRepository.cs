using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface ITransactionRepository
{
    Task<Transaction[]> Get(int[] accountIds, int[] categoryIds, DateTime from, DateTime to);

    //Task<Transaction[]> Get(int accountId, int[] categoryIds, DateTime from, DateTime to);

    //Task<Transaction[]> Get(int accountId, int[] categoryIds);

    //Task<Transaction[]> Get(int accountId);

    Task<Transaction[]> Get(int[] accountIds, int[] categoryIds);

    Task<Transaction[]> Get(int[] accountIds);

    Task<int> Add(Transaction transaction);

    Task Update(Transaction transaction);

    Task Delete(int transactionId);
}
