using MKeeper.Domain.Exceptions;
using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;
using MKeeper.Domain.Services;
using MKeeper.Domain.Common.CustomResults;
using MKeeper.BusinessLogic.CustomResults;

namespace MKeeper.BusinessLogic.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Result<int>> Create(Account account)
    {
        if (account is null)
        {
            throw new ArgumentNullException(nameof(account));
        }
        var isInvalid = account.User is null || account.Currency == null;
        if (isInvalid)
        {
            return new InvalidAccountResult<int>($"Invalid Account object: {account}");
        }
        var accountId = await _accountRepository.Add(account);
        return new SuccessResult<int>(accountId);
    }

    public async Task<Result<Account[]>> Get(int userId)
    {
        if (userId <= 0)
        {
            return new InvalidIdResult<Account[]>($"Invalid userId value: {userId}");
        }
        var accounts = await _accountRepository.Get(userId);
        return new SuccessResult<Account[]>(accounts);
    }

    public async Task<Result> Delete(int accountId)
    {
        if (accountId <= 0)
        {
            return new InvalidIdResult($"Invalid accountId value: {accountId}");
        }
        await _accountRepository.Delete(accountId);
        return new SuccessResult();
    }

    public async Task<Result> Update(Account account)
    {
        if (account is null)
        {
            throw new ArgumentNullException(nameof(account));
        }
        var isInvalid = account.User is null || account.Currency == null || account.Id <= 0;
        if (isInvalid)
        {
            return new InvalidAccountResult($"Invalid account object:\n{account}");
        }
        await _accountRepository.Update(account);
        return new SuccessResult();
    }
}
