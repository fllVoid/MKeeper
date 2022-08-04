using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;

namespace MKeeper.DataAccess.PSQL.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    public async Task<int> Add(Currency currency)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(int currencyId)
    {
        throw new NotImplementedException();
    }

    public async Task<Currency[]> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<Currency> GetByAlphaCode(string alphaCode)
    {
        throw new NotImplementedException();
    }

    public async Task<Currency> GetByNumericCode(string numericCode)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Currency currency)
    {
        throw new NotImplementedException();
    }
}
