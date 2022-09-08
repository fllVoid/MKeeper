using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface ICurrencyRepository
{
    Task<Currency> GetByAlphaCodeAsync(string alphaCode, CancellationToken cancellationToken = default);

    Task<Currency> GetByNumericCodeAsync(string numericCode, CancellationToken cancellationToken = default);

    Task<Currency[]> GetAllAsync(CancellationToken cancellationToken = default);

    Task<int> AddAsync(Currency currency, CancellationToken cancellationToken = default);

    Task UpdateAsync(Currency currency, CancellationToken cancellationToken = default);

    Task UpdateAsync(Currency[] currencies, CancellationToken cancellationToken = default);

    Task DeleteAsync(int currencyId, CancellationToken cancellationToken = default);
}
