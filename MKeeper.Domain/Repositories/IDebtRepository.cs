using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface IDebtRepository
{
    Task<Debt[]> Get(int accountId);

    Task<Debt[]> GetAll();

    Task<int> Add(Debt debt);

    Task Update(Debt debt);

    Task Delete(int debtId);
}
