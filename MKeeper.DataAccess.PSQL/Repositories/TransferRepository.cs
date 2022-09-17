using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace MKeeper.DataAccess.PSQL.Repositories;

public class TransferRepository : ITransferRepository
{
    private readonly MKeeperDbContext _context;
    private readonly IMapper _mapper;

    public TransferRepository(MKeeperDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Add(Transfer transfer, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<Entities.Transfer>(transfer);
        await _context.Transfers.AddAsync(mapped, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return mapped.Id;
    }

    public async Task Delete(int transferId, CancellationToken cancellationToken)
    {
        var entity = new Entities.Transfer() { Id = transferId };
        _context.Transfers.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Transfer[]> Get(int[] accountIds, DateTime from, DateTime to, CancellationToken cancellationToken)
    {
        var entities = await _context.Transfers
            .AsNoTracking()
            .Where(x => x.CreationDate >= from && x.CreationDate < to && accountIds.Contains(x.SourceAccount.Id))
            .ToArrayAsync(cancellationToken);
        var mapped = _mapper.Map<Transfer[]>(entities);
        return mapped;
    }

    public async Task<Transfer[]> Get(int[] accountIds, CancellationToken cancellationToken)
    {
        var entities = await _context.Transfers
            .AsNoTracking()
            .Where(x => accountIds.Contains(x.SourceAccount.Id))
            .ToArrayAsync(cancellationToken);
        var mapped = _mapper.Map<Transfer[]>(entities);
        return mapped;
    }

    public async Task Update(Transfer transfer, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<Entities.Transfer>(transfer);
        _context.Transfers.Update(mapped);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
