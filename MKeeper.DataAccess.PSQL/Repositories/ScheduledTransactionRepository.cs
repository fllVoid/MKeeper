using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;

namespace MKeeper.DataAccess.PSQL.Repositories;

public class ScheduledTransactionRepository : IScheduledTransactionRepository
{
    private readonly MKeeperDbContext _context;

    public ScheduledTransactionRepository(MKeeperDbContext context) => _context = context;

    public async Task<int> Add(ScheduledTransaction scheduledTransaction)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(int sheduledTransactionId)
    {
        throw new NotImplementedException();
    }

    public async Task<ScheduledTransaction[]> Get(int[] accountIds)
    {
        throw new NotImplementedException();
    }

    public async Task Update(ScheduledTransaction scheduledTransaction)
    {
        throw new NotImplementedException();
    }
}
