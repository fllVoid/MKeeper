using System;
using Moq;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using MKeeper.Infrastructure;
using Microsoft.Extensions.Logging;
using MKeeper.Domain.Models;
using MKeeper.Domain.Common.CustomResults;
using FluentAssertions;

namespace MKeeper.BusinessLogic.XTests.ApiClientTests;

public class CurrencyApiClientTests
{
    private static readonly string _correctApiResponsePath = "ApiClientTests/CorrectApiResponse.json";
    private static readonly string _baseCurrency = "USD";
    private readonly ILogger _fakeLogger;
    private readonly CurrencyApiClientConfig _apiConfig;

    public CurrencyApiClientTests()
    {
        _fakeLogger = new Mock<ILogger>().Object;
        _apiConfig = new CurrencyApiClientConfig("12345");
    }

    [Fact]
    public async Task GetFreshCurrencies_ApiResponseIsValid_ShouldReturnRefreshedCurrencies()
    {
        var apiResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(File.ReadAllText(_correctApiResponsePath))
        };
        var httpClient = new HttpClient(new MockedHttpMessageHandler(apiResponse));
        var apiClient = new CurrencyApiClient(httpClient, _apiConfig, _fakeLogger);
        var expiredCurrencies = new[]{
            new Currency() { AlphabeticCode = "RUB", ExchangeRate = 60M },
            new Currency() { AlphabeticCode = "EUR", ExchangeRate = 0.9M },
            new Currency() { AlphabeticCode = "USD", ExchangeRate = 1M }
        };

        var result = await apiClient.GetFreshCurrencies(expiredCurrencies);

        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Should().BeOfType<SuccessResult<Currency[]>>();
        result.Should()
            .Match<SuccessResult<Currency[]>>(x => x.Data.Length == expiredCurrencies.Length);
        IsCurrenciesRefreshed(expiredCurrencies, result.Data, _baseCurrency)
            .Should().BeTrue();
    }

    [Fact]
    public async Task GetFreshCurrencies_ApiResponseIsNull_ShouldReturnErrorResult()
    {
        HttpResponseMessage apiResponse = null;
        var httpClient = new HttpClient(new MockedHttpMessageHandler(apiResponse));
        var apiClient = new CurrencyApiClient(httpClient, _apiConfig, _fakeLogger);

        var result = await apiClient.GetFreshCurrencies(new Currency[1]);  

        result.Should().NotBeNull();
        result.Should().BeOfType<ErrorResult<Currency[]>>();
        result.Success.Should().BeFalse();
    }

    [Fact]
    public async Task GetFreshCurrencies_ApiResponseIsEmptyString_ShouldReturnErrorResult()
    {
        HttpResponseMessage apiResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("")
        };
        var httpClient = new HttpClient(new MockedHttpMessageHandler(apiResponse));
        var apiClient = new CurrencyApiClient(httpClient, _apiConfig, _fakeLogger);

        var result = await apiClient.GetFreshCurrencies(new Currency[1]);

        result.Should().NotBeNull();
        result.Should().BeOfType<ErrorResult<Currency[]>>();
        result.Success.Should().BeFalse();
    }

    [Fact]
    public async Task GetFreshCurrencies_ApiResponseIsInvalid_ShouldReturnErrorResult()
    {
        HttpResponseMessage apiResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("abracadabra")
        };
        var httpClient = new HttpClient(new MockedHttpMessageHandler(apiResponse));
        var apiClient = new CurrencyApiClient(httpClient, _apiConfig, _fakeLogger);

        var result = await apiClient.GetFreshCurrencies(new Currency[1]);

        result.Should().NotBeNull();
        result.Should().BeOfType<ErrorResult<Currency[]>>();
        result.Success.Should().BeFalse();
    }

    [Theory]
    [InlineData(HttpStatusCode.BadGateway)]
    [InlineData(HttpStatusCode.InternalServerError)]
    [InlineData(HttpStatusCode.GatewayTimeout)]
    [InlineData(HttpStatusCode.RequestTimeout)]
    [InlineData(HttpStatusCode.TooManyRequests)]
    public async Task GetFreshCurrencies_ApiReturnsRetryableStatusCode_ShouldReturnRetryResult(HttpStatusCode statusCode)
    {
        HttpResponseMessage apiResponse = new HttpResponseMessage(statusCode);
        var httpClient = new HttpClient(new MockedHttpMessageHandler(apiResponse));
        var apiClient = new CurrencyApiClient(httpClient, _apiConfig, _fakeLogger);

        var result = await apiClient.GetFreshCurrencies(new Currency[1]);

        result.Should().NotBeNull();
        result.Should().BeOfType<RetryResult<Currency[]>>();
        result.Success.Should().BeFalse();
    }

    [Theory]
    [InlineData(HttpStatusCode.NoContent)]
    [InlineData(HttpStatusCode.NotFound)]
    public async Task GetFreshCurrencies_ApiReturnsNonRetryableStatusCode_ShouldReturnErrorResult(HttpStatusCode statusCode)
    {
        HttpResponseMessage apiResponse = new HttpResponseMessage(statusCode);
        var httpClient = new HttpClient(new MockedHttpMessageHandler(apiResponse));
        var apiClient = new CurrencyApiClient(httpClient, _apiConfig, _fakeLogger);

        var result = await apiClient.GetFreshCurrencies(new Currency[1]);

        result.Should().NotBeNull();
        result.Should().BeOfType<ErrorResult<Currency[]>>();
        result.Success.Should().BeFalse();
    }


    [Fact]
    public async Task GetFreshCurrencies_OnHttpRequestException_ShouldReturnRetryResult()
    {
        var httpClient = new HttpClient(new MockedHttpMessageHandler(() => throw new HttpRequestException()));
        var apiClient = new CurrencyApiClient(httpClient, _apiConfig, _fakeLogger);

        var result = await apiClient.GetFreshCurrencies(new Currency[1]);

        result.Should().NotBeNull();
        result.Should().BeOfType<RetryResult<Currency[]>>();
        result.Success.Should().BeFalse();
    }

    [Fact]
    public async Task GetFreshCurrencies_OnTaskCanceledException_ShouldReturnRetryResult()
    {
        HttpResponseMessage apiResponse = new HttpResponseMessage(HttpStatusCode.OK);
        var httpClient = new HttpClient(new MockedHttpMessageHandler(apiResponse));
        var apiClient = new CurrencyApiClient(httpClient, _apiConfig, _fakeLogger);
        var cancelledToken = new CancellationToken(true);
        
        var result = await apiClient.GetFreshCurrencies(new Currency[1], cancelledToken);

        result.Should().NotBeNull();
        result.Should().BeOfType<RetryResult<Currency[]>>();
        result.Success.Should().BeFalse();
    }

    [Fact]
    public async Task GetFreshCurrencies_expiredCurrenciesArgumentIsNull_ShouldThrowArgumentException()
    {
        HttpResponseMessage apiResponse = null;
        var httpClient = new HttpClient(new MockedHttpMessageHandler(apiResponse));
        var apiClient = new CurrencyApiClient(httpClient, _apiConfig, _fakeLogger);

        var task = await Assert.ThrowsAsync<ArgumentException>(
            async () =>  await apiClient.GetFreshCurrencies(null));
    }

    [Fact]
    public async Task GetFreshCurrencies_expiredCurrenciesArgumentIsEmpty_ShouldThrowArgumentException()
    {
        HttpResponseMessage apiResponse = null;
        var httpClient = new HttpClient(new MockedHttpMessageHandler(apiResponse));
        var apiClient = new CurrencyApiClient(httpClient, _apiConfig, _fakeLogger);

        var task = await Assert.ThrowsAsync<ArgumentException>(
            async () => await apiClient.GetFreshCurrencies(new Currency[0]));
    }

    private bool IsCurrenciesRefreshed(Currency[] expiredCurrencies,
        Currency[] freshCurrencies,
        string baseCurrencyAlphabeticCode)
    {
        foreach(var c in expiredCurrencies)
        {
            if (c.AlphabeticCode == baseCurrencyAlphabeticCode)
                continue;
            var twin = freshCurrencies.First(x => x.AlphabeticCode == c.AlphabeticCode);
            if (twin.ExchangeRate == c.ExchangeRate)
                return false;
        }
        return true;
    }
}

public class MockedHttpMessageHandler : HttpMessageHandler
{
    private readonly Func<Task<HttpResponseMessage>> _responseFactory;
    private readonly HttpResponseMessage _httpResponseMessage;

    public MockedHttpMessageHandler(HttpResponseMessage httpResponseMessage)
    {
        _httpResponseMessage = httpResponseMessage;
    }

    public MockedHttpMessageHandler(Func<Task<HttpResponseMessage>> responseFactory)
    {
        _responseFactory = responseFactory ?? throw new ArgumentNullException(nameof(responseFactory));
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (_responseFactory != null)
        {
            return await _responseFactory.Invoke();
        }

        return _httpResponseMessage;
    }
}
