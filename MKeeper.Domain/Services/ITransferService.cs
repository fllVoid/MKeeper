using MKeeper.Domain.Models;
using MKeeper.Domain.Common.CustomResults;

namespace MKeeper.Domain.Services;

public interface ITransferService
{
    Task<Result<int>> Create(Transfer transfer);

    Task<Result<Transfer[]>> Get(int[] accountIds, DateTime from, DateTime to);

    Task<Result<Transfer[]>> Get(int[] accountIds);

    Task<Result> Update(Transfer transfer);

    Task<Result> Delete(int transferId);
}
