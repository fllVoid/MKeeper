using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface ICurrencyRepository
{
    Task<Currency> GetByAlphaCode(string alphaCode);

    Task<Currency> GetByNumericCode(string numericCode);

    Task<Currency[]> GetAll();

    Task<int> AddCurrency(Currency currency);

    Task UpdateCurrency(Currency currency);

    Task DeleteCurrency(int currencyId);
}
