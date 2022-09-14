namespace MKeeper.Infrastructure;

public class CurrencyApiClientConfig
{
    public string ApiKey { get; private set; }
    public CurrencyApiClientConfig(string apiKey)
    {
        ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
    }
}
