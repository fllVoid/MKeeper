using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface IAccountRepository
{
    Task<Account[]> GetAccounts(int userId);

    Task<Account[]> GetAllAccounts();

    Task<int> AddAccount(Account account);

    Task UpdateAccount(Account account);

    Task DeleteAccount(int accountId);
}
