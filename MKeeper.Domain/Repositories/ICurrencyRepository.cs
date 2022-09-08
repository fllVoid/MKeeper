using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface ICurrencyRepository
{
    Task<Currency> GetByAlphaCodeAsync(string alphaCode);

    Task<Currency> GetByNumericCodeAsync(string numericCode);

    Task<Currency[]> GetAllAsync();

    Task<int> AddAsync(Currency currency);

    Task UpdateAsync(Currency currency);

    Task UpdateAsync(Currency[] currencies);

    Task DeleteAsync(int currencyId);
}
