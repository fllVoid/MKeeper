using MKeeper.Domain.Exceptions;
using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;
using MKeeper.Domain.Services;
using MKeeper.Domain.Common.CustomResults;
using MKeeper.BusinessLogic.CustomResults;

namespace MKeeper.BusinessLogic.Services;

public class DebtService : IDebtService
{
    private readonly IDebtRepository _debtRepository;

    public DebtService(IDebtRepository debtRepository)
    {
        _debtRepository = debtRepository;
    }

    public async Task<Result<int>> Create(Debt debt)
    {
        if (debt == null)
        {
            throw new ArgumentNullException(nameof(debt));
        }
        var isInvalid = debt.RepaymentDate < debt.CreationDate || string.IsNullOrWhiteSpace(debt.SubjectName) 
            || debt.SourceAccount == null;
        if (isInvalid)
        {
            return new InvalidDebtResult<int>($"Invalid Debt object:\n{debt}");
        }
        var id = await _debtRepository.Add(debt);
        return new SuccessResult<int>(id);
    }

    public async Task<Result<Debt[]>> Get(int accountId)
    {
        if (accountId <= 0)
        {
            return new InvalidIdResult<Debt[]>($"Invalid accountId value: {accountId}");
        }
        var debts = await _debtRepository.Get(accountId);
        return new SuccessResult<Debt[]>(debts);
    }

    public async Task<Result> Update(Debt debt)
    {
        if (debt == null)
        {
            throw new ArgumentNullException(nameof(debt));
        }
        var isInvalid = debt.RepaymentDate < debt.CreationDate || string.IsNullOrWhiteSpace(debt.SubjectName)
            || debt.SourceAccount == null || debt.Id <= 0;
        if (isInvalid)
        {
            return new InvalidDebtResult($"Invalid Debt object: {debt}");
        }
        await _debtRepository.Update(debt);
        return new SuccessResult();
    }

    public async Task<Result> Delete(int debtId)
    {
        if (debtId <= 0)
        {
            return new InvalidIdResult($"Invalid debtId value: {debtId}");
        }
        await _debtRepository.Delete(debtId);
        return new SuccessResult();
    }
}
