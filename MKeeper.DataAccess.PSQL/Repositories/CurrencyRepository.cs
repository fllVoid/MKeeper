using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;

namespace MKeeper.DataAccess.PSQL.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    public Task<Currency[]> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Currency> GetByAlphaCode(string alphaCode)
    {
        throw new NotImplementedException();
    }

    public Task<Currency> GetByNumericCode(string numericCode)
    {
        throw new NotImplementedException();
    }

    public Task<int> AddCurrency(Currency currency)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCurrency(Currency currency)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCurrency(int currencyId)
    {
        throw new NotImplementedException();
    }
}
