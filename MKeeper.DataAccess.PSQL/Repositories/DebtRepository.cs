using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;

namespace MKeeper.DataAccess.PSQL.Repositories;

public class DebtRepository : IDebtRepository
{
    public async Task<int> Add(Debt debt)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(int debtId)
    {
        throw new NotImplementedException();
    }

    public async Task<Debt[]> Get(int accountId)
    {
        throw new NotImplementedException();
    }

    public async Task<Debt[]> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task Update(Debt debt)
    {
        throw new NotImplementedException();
    }
}
