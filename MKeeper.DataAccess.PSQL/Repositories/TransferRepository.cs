using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;

namespace MKeeper.DataAccess.PSQL.Repositories;

public class TransferRepository : ITransferRepository
{
    private readonly MKeeperDbContext _context;

    public TransferRepository(MKeeperDbContext context) => _context = context;

    public async Task<int> Add(Transfer Transfer)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(int TransferId)
    {
        throw new NotImplementedException();
    }

    public async Task<Transfer[]> Get(int[] accountIds, DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }

    public async Task<Transfer[]> Get(int[] accountIds)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Transfer Transfer)
    {
        throw new NotImplementedException();
    }
}
