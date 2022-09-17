using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface IAccountRepository
{
    Task<Account[]> Get(int userId, CancellationToken cancellationToken);

    Task<Account[]> GetAll(CancellationToken cancellationToken);

    Task<int> Add(Account account, CancellationToken cancellationToken);

    Task Update(Account account, CancellationToken cancellationToken);

    Task Delete(int accountId, CancellationToken cancellationToken);
}
