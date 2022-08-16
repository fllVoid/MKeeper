using MKeeper.Domain.Exceptions;
using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;
using MKeeper.Domain.Services;

namespace MKeeper.BusinessLogic.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<int> Create(Account account)
    {
        if (account is null)
        {
            throw new ArgumentNullException(nameof(account));
        }
        var isInvalid = account.User is null || account.Currency == null;
        if (isInvalid)
        {
            throw new BusinessException(nameof(account));
        }
        var accountId = await _accountRepository.Add(account);
        return accountId;
    }

    public async Task<Account[]> Get(int userId)
    {
        if (userId <= 0)
        {
            throw new ArgumentException(nameof(userId));
        }
        var accounts = await _accountRepository.Get(userId);
        return accounts;
    }

    public async Task Delete(int accountId)
    {
        if (accountId <= 0)
        {
            throw new ArgumentException(nameof(accountId));
        }
        await _accountRepository.Delete(accountId);
    }

    public async Task Update(Account account)
    {
        if (account is null)
        {
            throw new ArgumentNullException(nameof(account));
        }
        var isInvalid = account.User is null || account.Currency == null || account.Id <= 0;
        if (isInvalid)
        {
            throw new BusinessException(nameof(account));
        }
        await _accountRepository.Update(account);
    }
}
