using MKeeper.Domain.Exceptions;
using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;
using MKeeper.Domain.Services;

namespace MKeeper.BusinessLogic.Services;

public class TransferService : ITransferService
{
    private readonly ITransferRepository _transferRepository;

    public TransferService(ITransferRepository transferRepository)
    {
        _transferRepository = transferRepository;
    }

    public async Task<int> Create(Transfer transfer)
    {
        if (transfer == null)
        {
            throw new ArgumentNullException(nameof(transfer));
        }
        var isInvalid = transfer.DestinationAccount == null || transfer.SourceAccount == null;
        if (isInvalid)
        {
            throw new BusinessException(nameof(transfer));
        }
        return await _transferRepository.Add(transfer);
    }

    public async Task<Transfer[]> Get(int[] accountIds, DateTime from, DateTime to)
    {
        if (accountIds.Any(x => x <= 0))
        {
            throw new ArgumentException(nameof(accountIds));
        }
        if (from > to)
        {
            throw new BusinessException(nameof(to));
        }
        var transfers = await _transferRepository.Get(accountIds, from, to);
        return transfers;
    }

    public async Task<Transfer[]> Get(int[] accountIds)
    {
        if (accountIds.Any(x => x <= 0))
        {
            throw new ArgumentException(nameof(accountIds));
        }
        var transfers = await _transferRepository.Get(accountIds);
        return transfers;
    }

    public async Task Update(Transfer transfer)
    {
        if (transfer == null)
        {
            throw new ArgumentNullException(nameof(transfer));
        }
        var isInvalid = transfer.DestinationAccount == null || transfer.SourceAccount == null;
        if (isInvalid)
        {
            throw new BusinessException(nameof(transfer));
        }
        await _transferRepository.Update(transfer);
    }

    public async Task Delete(int transferId)
    {
        if (transferId <= 0)
        {
            throw new ArgumentException(nameof(transferId));
        }
        await _transferRepository.Delete(transferId);
    }
}
