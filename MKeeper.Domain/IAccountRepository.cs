using MKeeper.Domain.Models;

namespace MKeeper.Domain;

public interface IAccountRepository
{
    Account GetAccount(int accountId);
    IEnumerable<Account> GetAccounts(int userId);
    IEnumerable<Account> GetAllAccounts();
    void AddAccount(Account account);
    void UpdateAccount(Account account);
    void DeleteAccount(int accountId);
}
