using MKeeper.Domain.Models;


namespace MKeeper.Domain.Services;

public interface IDebtService
{
    Task<int> Create(Debt debt);

    Task<Debt[]> Get(int accountId);

    Task Update(Debt debt);

    Task Delete(int debtId);
}
