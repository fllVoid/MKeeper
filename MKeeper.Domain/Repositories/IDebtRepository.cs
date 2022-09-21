using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface IDebtRepository
{
    Task<Debt[]> Get(int accountId, CancellationToken cancellationToken = default);

    Task<Debt[]> GetAll(CancellationToken cancellationToken = default);

    Task<int> Add(Debt debt, CancellationToken cancellationToken = default);

    Task Update(Debt debt, CancellationToken cancellationToken = default);

    Task Delete(int debtId, CancellationToken cancellationToken = default);
}
