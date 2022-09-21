using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface IAccountRepository
{
    Task<Account[]> Get(int userId, CancellationToken cancellationToken = default);

    Task<Account[]> GetAll(CancellationToken cancellationToken = default);

    Task<int> Add(Account account, CancellationToken cancellationToken = default);

    Task Update(Account account, CancellationToken cancellationToken = default);

    Task Delete(int accountId, CancellationToken cancellationToken = default);
}
