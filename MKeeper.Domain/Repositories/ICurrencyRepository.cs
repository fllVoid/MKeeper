using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface ICurrencyRepository
{
    Task<Currency> GetByAlphaCode(string alphaCode);

    Task<Currency> GetByNumericCode(string numericCode);

    Task<Currency[]> GetAll();

    Task<int> Add(Currency currency);

    Task Update(Currency currency);

    Task Update(Currency[] currencies);

    Task Delete(int currencyId);
}
