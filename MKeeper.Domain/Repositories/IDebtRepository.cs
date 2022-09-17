using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface IDebtRepository
{
    Task<Debt[]> Get(int accountId, CancellationToken cancellationToken);

    Task<Debt[]> GetAll(CancellationToken cancellationToken);

    Task<int> Add(Debt debt, CancellationToken cancellationToken);

    Task Update(Debt debt, CancellationToken cancellationToken);

    Task Delete(int debtId, CancellationToken cancellationToken);
}
