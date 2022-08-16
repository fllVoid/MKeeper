using MKeeper.Domain.Exceptions;
using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;
using MKeeper.Domain.Services;

namespace MKeeper.BusinessLogic.Services;

public class DebtService : IDebtService
{
    private readonly IDebtRepository _debtRepository;

    public DebtService(IDebtRepository debtRepository)
    {
        _debtRepository = debtRepository;
    }

    public async Task<int> Create(Debt debt)
    {
        if (debt == null)
        {
            throw new ArgumentNullException(nameof(debt));
        }
        var isInvalid = debt.RepaymentDate < debt.CreationDate || string.IsNullOrWhiteSpace(debt.SubjectName) 
            || debt.SourceAccount == null;
        if (isInvalid)
        {
            throw new BusinessException(nameof(debt));
        }
        return await _debtRepository.Add(debt);
    }

    public async Task<Debt[]> Get(int accountId)
    {
        if (accountId <= 0)
        {
            throw new ArgumentException(nameof(accountId));
        }
        var debts = await _debtRepository.Get(accountId);
        return debts;
    }

    public async Task Update(Debt debt)
    {
        if (debt == null)
        {
            throw new ArgumentNullException(nameof(debt));
        }
        var isInvalid = debt.RepaymentDate < debt.CreationDate || string.IsNullOrWhiteSpace(debt.SubjectName)
            || debt.SourceAccount == null;
        if (isInvalid)
        {
            throw new BusinessException(nameof(debt));
        }
        await _debtRepository.Update(debt);
    }

    public async Task Delete(int debtId)
    {
        if (debtId <= 0)
        {
            throw new ArgumentException(nameof(debtId));
        }
        await _debtRepository.Delete(debtId);
    }
}
