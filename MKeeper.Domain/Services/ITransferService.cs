using MKeeper.Domain.Models;

namespace MKeeper.Domain.Services;

public interface ITransferService
{
    Task<int> Create(Transfer transfer);

    Task Update(Transfer transfer);

    Task Delete(int transferId);
}
