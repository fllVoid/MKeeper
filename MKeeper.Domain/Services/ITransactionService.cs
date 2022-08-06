using MKeeper.Domain.Models;

namespace MKeeper.Domain.Services;

public interface ITransactionService
{
    Task<int> Create(Transaction transaction);

    Task Update(Transaction transaction);

    Task Delete(int transactionId);
}
