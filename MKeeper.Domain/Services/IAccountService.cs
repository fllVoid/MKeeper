using MKeeper.Domain.Models;
using MKeeper.Domain.Common.CustomResults;

namespace MKeeper.Domain.Services;

public interface IAccountService
{
    Task<Result<int>> Create(Account account);

    Task<Result<Account[]>> Get(int userId);

    Task<Result> Update(Account account);

    Task<Result> Delete(int accountId);
}
