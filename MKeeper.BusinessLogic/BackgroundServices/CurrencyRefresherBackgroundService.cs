using Microsoft.Extensions.Hosting;
using MKeeper.Domain.ApiClients;
using MKeeper.Domain.Repositories;
using MKeeper.Domain.Common.CustomResults;
using MKeeper.Domain.Models;
using Microsoft.Extensions.Logging;

namespace MKeeper.BusinessLogic.BackgroundServices;

public class CurrencyRefresherBackgroundService : IHostedService, IAsyncDisposable
{
	private const int _numberOfTries = 5;
	private readonly ICurrencyApiClient _apiClient;
	private readonly ICurrencyRepository _repository;
    private readonly ILogger _logger;
    private Timer? _timer;

	public CurrencyRefresherBackgroundService(ICurrencyApiClient apiClient,
		ICurrencyRepository repository,
		ILogger logger)
	{
		_apiClient = apiClient;
		_repository = repository;
		_logger = logger;
	}

    public Task StartAsync(CancellationToken cancellationToken)
	{
		_timer = new Timer(async _ => await OnTimerFiredAsync(cancellationToken),
			null,
			TimeSpan.FromHours(0),
			TimeSpan.FromHours(1));
		return Task.CompletedTask;
	}

	public async Task StopAsync(CancellationToken cancellationToken)
	{
		await DisposeAsync();
	}

	private async Task OnTimerFiredAsync(CancellationToken stoppingToken)
	{
		try
		{
			var existingCurrencies = await _repository.GetAllAsync(stoppingToken);
			var tries = _numberOfTries;
			while (tries > 0)
			{
				if (stoppingToken.IsCancellationRequested)
                {
					stoppingToken.ThrowIfCancellationRequested();
                }
				var result = await _apiClient.GetFreshCurrencies(existingCurrencies, stoppingToken);
				if (result is RetryResult<Currency[]>)
                {
					tries--;
					continue;
                }
                if (result is ErrorResult<Currency[]>)
                {
                    break;
                }

				Currency[] freshCurrencies = result.Data;
				await _repository.UpdateAsync(freshCurrencies, stoppingToken);
				break;
			}
		}
		catch (Exception exception)
		{
			_logger.LogError(exception, "UnhandledException when refreshing currencies");
		}
	}

	public async ValueTask DisposeAsync()
	{
		if (_timer is IAsyncDisposable timer)
		{
			await timer.DisposeAsync();
		}
		_timer = null;
	}
}
