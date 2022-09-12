using MKeeper.Domain.ApiClients;
using MKeeper.Domain.Models;
using System.Globalization;
using System.Text.Json;

namespace MKeeper.Infrastructure;

public class CurrencyApiClient : ICurrencyApiClient
{
    private readonly HttpClient _httpClient;
    private readonly CurrencyApiClientConfig _config;

    public CurrencyApiClient(HttpClient httpClient, CurrencyApiClientConfig config)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public async Task<Currency[]> GetFreshCurrencies(Currency[] currencies, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"https://openexchangerates.org/api/latest.json?app_id={_config.ApiKey}");
        using (var response = await _httpClient.SendAsync(request, cancellationToken))
        {
            var jsonContent = await response.Content.ReadAsStringAsync();
            var responseObj = ParseResponseObject(jsonContent);

        }

    }

    private ApiResponse ParseResponseObject(string jsonContent)
    {
        /* method looks like this because the API response contains 
         * many objects instead of array of objects */
        var jsonDoc = JsonDocument.Parse(jsonContent);
        var root = jsonDoc.RootElement;
        var rates = root.GetProperty("rates");
        var ratesEnum = rates.EnumerateObject();

        //make array from many objects
        var ratesArray = ratesEnum
            .Select(p => new ApiResponse.CurrencyRate()
            {
                AlphabeticCode = p.Name,
                Rate = double.Parse(p.Value.ToString(), CultureInfo.InvariantCulture)
            })
            .ToArray();

        //init response object manually 
        var timestampPropValue = root.GetProperty("timestamp").GetInt64();
        var basePropValue = root.GetProperty("base").GetString();
        ApiResponse apiResponseObj = new()
        {
            Timestamp = timestampPropValue,
            Base = basePropValue ?? throw new ArgumentNullException(nameof(basePropValue)),
            Rates = ratesArray
        };
        return apiResponseObj;
    }

    class ApiResponse
    {
        public long Timestamp { get; set; }
        public string Base { get; set; }
        public CurrencyRate[] Rates { get; set; }

        public class CurrencyRate
        {
            public string AlphabeticCode { get; set; }
            public double Rate { get; set; }
        }
    }
}
