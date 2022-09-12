using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKeeper.Infrastructure;

public class CurrencyApiClientConfig
{
    public string ApiKey { get; private set; }
    public CurrencyApiClientConfig(string apiKey)
    {
        ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
    }
}
