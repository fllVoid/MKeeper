using MKeeper.Domain.Exceptions;
using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;
using MKeeper.Domain.Services;
using MKeeper.Domain.Common.CustomResults;
using MKeeper.BusinessLogic.CustomResults;

namespace MKeeper.BusinessLogic.Services;

public class TransferService : ITransferService
{
    private readonly ITransferRepository _transferRepository;

    public TransferService(ITransferRepository transferRepository)
    {
        _transferRepository = transferRepository;
    }

    public async Task<Result<int>> Create(Transfer transfer)
    {
        if (transfer == null)
        {
            throw new ArgumentNullException(nameof(transfer));
        }
        var isInvalid = transfer.DestinationAccount == null || transfer.SourceAccount == null;
        if (isInvalid)
        {
            return new InvalidTransferResult<int>($"Invalid Transfer object: {transfer}");
        }
        var id = await _transferRepository.Add(transfer);
        return new SuccessResult<int>(id);
    }

    public async Task<Result<Transfer[]>> Get(int[] accountIds, DateTime from, DateTime to)
    {
        if (accountIds.Any(x => x <= 0))
        {
            return new InvalidIdResult<Transfer[]>("Invalid accountId in accountIds array");
        }
        if (from > to)
        {
            return new InvalidDateResult<Transfer[]>($"'from' was less than 'to':\nfrom: {from}\nto: {to}");
        }
        var transfers = await _transferRepository.Get(accountIds, from, to);
        return new SuccessResult<Transfer[]>(transfers);
    }

    public async Task<Result<Transfer[]>> Get(int[] accountIds)
    {
        if (accountIds.Any(x => x <= 0))
        {
            return new InvalidIdResult<Transfer[]>("Invalid accountId in accountIds array");
        }
        var transfers = await _transferRepository.Get(accountIds);
        return new SuccessResult<Transfer[]>(transfers);
    }

    public async Task<Result> Update(Transfer transfer)
    {
        if (transfer == null)
        {
            throw new ArgumentNullException(nameof(transfer));
        }
        var isInvalid = transfer.Id <= 0 || transfer.DestinationAccount == null 
            || transfer.SourceAccount == null;
        if (isInvalid)
        {
            return new InvalidTransferResult($"Invalid Transfer object: {transfer}");
        }
        await _transferRepository.Update(transfer);
        return new SuccessResult();
    }

    public async Task<Result> Delete(int transferId)
    {
        if (transferId <= 0)
        {
            return new InvalidIdResult($"Invalid transferId value: {transferId}");
        }
        await _transferRepository.Delete(transferId);
        return new SuccessResult();
    }
}
