using MKeeper.Domain.ApiClients;
using MKeeper.Domain.Models;
using MKeeper.Domain.Common.CustomResults;
using System.Globalization;
using System.Net;
using System.Text.Json;
using MKeeper.Infrastructure.CustomResults;
using Microsoft.Extensions.Logging;
using MKeeper.Infrastructure.Exceptions;

namespace MKeeper.Infrastructure;

public class CurrencyApiClient : ICurrencyApiClient
{
    private static readonly HashSet<HttpStatusCode> _retryableStatusCodes = new HashSet<HttpStatusCode>
    {
        HttpStatusCode.InternalServerError,
        HttpStatusCode.GatewayTimeout,
        HttpStatusCode.RequestTimeout,
        HttpStatusCode.BadGateway,
        HttpStatusCode.TooManyRequests
    };

    private readonly HttpClient _httpClient;
    private readonly CurrencyApiClientConfig _config;
    private readonly ILogger _logger;

    public CurrencyApiClient(HttpClient httpClient, CurrencyApiClientConfig config, ILogger logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<Currency[]>> GetFreshCurrencies(Currency[] expiredCurrencies, CancellationToken cancellationToken = default)
    {
        if (expiredCurrencies is null || expiredCurrencies.Length == 0)
            throw new ArgumentException(nameof(expiredCurrencies));
        var request = new HttpRequestMessage(HttpMethod.Get, $"https://openexchangerates.org/api/latest.json?app_id={_config.ApiKey}");
        try
        {
            using (var response = await _httpClient.SendAsync(request, cancellationToken))
            {
                if (response.IsSuccessStatusCode)
                {
                    if (response.Content is null)
                    {
                        _logger.LogError("Response content was null with success status code.");
                        return new ErrorResult<Currency[]>("Response content was null with success status code.");
                    }
                    var jsonContent = await response.Content.ReadAsStringAsync();
                    var responseObj = ParseResponse(jsonContent);
                    var rates = responseObj.Rates;
                    var result = CreateFreshCurrencies(expiredCurrencies, responseObj.Rates);
                    return result;
                }
                if (_retryableStatusCodes.Contains(response.StatusCode))
                    return new RetryResult<Currency[]>($"Recieved retryable status code: {response.StatusCode}");
                _logger.LogWarning($"Recieved error status code: {response.StatusCode}");
                return new ErrorResult<Currency[]>($"Recieved error status code: {response.StatusCode}");
            }
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "HttpRequestException when calling the API");
            return new RetryResult<Currency[]>("HttpRequestException when calling the API");
        }
        catch (TaskCanceledException exception)
        {
            _logger.LogError(exception, "Task was canceled during call to API");
            return new RetryResult<Currency[]>("Task was canceled during call to API");
        }
        catch (BrokenJsonException exception)
        {
            _logger.LogError(exception, "Invalid json content");
            return new ErrorResult<Currency[]>("Invalid json content");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "UnhandledException when calling API");
            return new ErrorResult<Currency[]>("UnhandledException when calling API");
        }
    }

    private Result<Currency[]> CreateFreshCurrencies(Currency[] expiredCurrencies, ApiResponse.CurrencyRate[] rates)
    {
        var freshCurrencies = expiredCurrencies
                           .Select(c => new Currency(c))
                           .ToArray(); //making copies of existing currencies

        var notEverythingRefreshed = false;
        var tmpRateObj = new ApiResponse.CurrencyRate(); //obj for passing to BinarySearch
        for (int i = 0; i < freshCurrencies.Length; i++)
        {
            tmpRateObj.AlphabeticCode = freshCurrencies[i].AlphaCode;
            var index = Array.BinarySearch(rates, tmpRateObj, new CurrencyRateComparer());
            if (index >= 0)
                freshCurrencies[i].ExchangeRate = rates[index].Rate;
            else notEverythingRefreshed = true;
        }
        return notEverythingRefreshed ?
            new NotEverythingRefreshedResult<Currency[]>(freshCurrencies)
            : new SuccessResult<Currency[]>(freshCurrencies);
    }

    private ApiResponse ParseResponse(string jsonContent)
    {
        /* method looks like this because the API response contains 
         * many objects instead of array of objects */
        try
        {
            var jsonDoc = JsonDocument.Parse(jsonContent);
            var root = jsonDoc.RootElement;
            var rates = root.GetProperty("rates");
            var timestampPropValue = root.GetProperty("timestamp").GetInt64();
            var basePropValue = root.GetProperty("base").GetString();
            var ratesEnum = rates.EnumerateObject();

            //make array from many objects
            var ratesArray = ratesEnum
                .Select(p => new ApiResponse.CurrencyRate()
                {
                    AlphabeticCode = p.Name,
                    Rate = decimal.Parse(p.Value.ToString(), CultureInfo.InvariantCulture)
                })
                .ToArray();

            //init response object manually 
            ApiResponse apiResponseObj = new()
            {
                Timestamp = timestampPropValue,
                Base = basePropValue ?? throw new ArgumentNullException(nameof(basePropValue)),
                Rates = ratesArray
            };
            return apiResponseObj;
        }
        catch (Exception exception)
        {
            throw new BrokenJsonException(exception.Message);
        }
    }

    class ApiResponse
    {
        public long Timestamp { get; set; }
        public string Base { get; set; }
        public CurrencyRate[] Rates { get; set; }

        public class CurrencyRate
        {
            public string AlphabeticCode { get; set; }
            public decimal Rate { get; set; }
        }
    }

    class CurrencyRateComparer : IComparer<ApiResponse.CurrencyRate>
    {
        public int Compare(ApiResponse.CurrencyRate? x, ApiResponse.CurrencyRate? y)
        {
            return x.AlphabeticCode.CompareTo(y.AlphabeticCode);
        }
    }
}
