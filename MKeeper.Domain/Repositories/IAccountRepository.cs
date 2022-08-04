using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface IAccountRepository
{
    Task<Account[]> Get(int userId);

    Task<Account[]> GetAll();

    Task<int> Add(Account account);

    Task Update(Account account);

    Task Delete(int accountId);
}
