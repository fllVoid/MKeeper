using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace MKeeper.DataAccess.PSQL.Repositories;

public class ScheduledTransactionRepository : IScheduledTransactionRepository
{
    private readonly MKeeperDbContext _context;
    private readonly IMapper _mapper;

    public ScheduledTransactionRepository(MKeeperDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Add(ScheduledTransaction scheduledTransaction, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<Entities.ScheduledTransaction>(scheduledTransaction);
        await _context.ScheduledTransactions.AddAsync(mapped, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return mapped.Id;
    }

    public async Task Delete(int scheduledTransactionId, CancellationToken cancellationToken)
    {
        var entity = new Entities.ScheduledTransaction() { Id = scheduledTransactionId };
        _context.ScheduledTransactions.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<ScheduledTransaction[]> Get(int[] accountIds, CancellationToken cancellationToken)
    {
        //TODO bad sql query maybe will be generated here. think about
        var entities = await _context.ScheduledTransactions
            .AsNoTracking()
            .Where(x => accountIds.Contains(x.Id))
            .ToArrayAsync(cancellationToken);
        var mapped = _mapper.Map<ScheduledTransaction[]>(entities);
        return mapped;
    }

    public async Task Update(ScheduledTransaction scheduledTransaction, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<Entities.ScheduledTransaction>(scheduledTransaction);
        _context.ScheduledTransactions.Update(mapped);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
