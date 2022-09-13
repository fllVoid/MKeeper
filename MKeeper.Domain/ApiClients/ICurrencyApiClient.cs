using MKeeper.Domain.Models;
using MKeeper.Domain.Common.CustomResults;

namespace MKeeper.Domain.ApiClients
{
    public interface ICurrencyApiClient
    {
        Task<Result<Currency[]>> GetFreshCurrencies(Currency[] currencies, CancellationToken cancellationToken = default);
    }
}
