using MKeeper.Domain.Models;

namespace MKeeper.Domain;

public interface ICurrencyRepository
{
    Currency GetCurrencyById(int currencyId);
    Currency GetCurrencyByAlphaCode(string alphaCode);
    Currency GetCurrencyByNumericCode(string numericCode);
    IEnumerable<Currency> GetAllCurrencies();
    void AddCurrency(Currency currency);
    void DeleteCurrency(int currencyId);
    void UpdateCurrency(Currency currency);
}
