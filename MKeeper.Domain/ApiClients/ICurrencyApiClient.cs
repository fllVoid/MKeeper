using MKeeper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKeeper.Domain.ApiClients
{
    public interface ICurrencyApiClient
    {
        Task<Currency[]> GetFreshCurrencies(Currency[] currencies, CancellationToken cancellationToken = default);
    }
}
