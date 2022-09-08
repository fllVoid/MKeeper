using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKeeper.BusinessLogic.BackgroundServices;

public class ScheduledTransactionBackgroundService
{
    private Timer? _timer;

    private int MilliSecondsUntilNextHour()
    {
        var now = DateTime.UtcNow;
        var clearNextHour = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0, now.Kind)
            .AddHours(1);
        return (int)(clearNextHour - now).TotalMilliseconds;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(async _ => await OnTimerFiredAsync(cancellationToken),
            null,
            MilliSecondsUntilNextHour(),
            Timeout.Infinite);
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
        }
        catch (Exception exception)
        {
        }
        finally
        {
            _timer?.Change(MilliSecondsUntilNextHour(), Timeout.Infinite);
        }
    }
}
