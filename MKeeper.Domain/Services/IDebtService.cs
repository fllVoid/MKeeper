using MKeeper.Domain.Models;
using MKeeper.Domain.Common.CustomResults;

namespace MKeeper.Domain.Services;

public interface IDebtService
{
    Task<Result<int>> Create(Debt debt);

    Task<Result<Debt[]>> Get(int accountId);

    Task<Result> Update(Debt debt);

    Task<Result> Delete(int debtId);
}
