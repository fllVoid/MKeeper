using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MKeeper.Domain.ApiClients;
using MKeeper.Domain.Repositories;

namespace MKeeper.BusinessLogic.BackgroundServices;

public class CurrencyRefresherBackgroundService : IHostedService
{
    private readonly ICurrencyApiClient _apiClient;
    private readonly ICurrencyRepository _repository;
    private Timer? _timer;

    public CurrencyRefresherBackgroundService(ICurrencyApiClient apiClient, ICurrencyRepository repository)
    {
        _apiClient = apiClient;
        _repository = repository;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(async _ => await OnTimerFiredAsync(cancellationToken),
            null,
            TimeSpan.FromHours(0),
            TimeSpan.FromHours(1));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Dispose();
        return Task.CompletedTask;
    }

    private async Task OnTimerFiredAsync(CancellationToken stoppingToken)
    {
        try
        {
            //work
            var existingCurrencies = await _repository.GetAllAsync();
            var freshCurrencies = await _apiClient.GetFreshCurrencies(existingCurrencies);
            await _repository.UpdateAsync(freshCurrencies);
        }
        catch (Exception ex)
        {
            //TODO at least, log exception here
        }
        finally
        {
        }
    }
}
