using MKeeper.Domain.Models;

namespace MKeeper.Domain;

public interface IDebtRepository
{
    IEnumerable<Debt> GetDebts(int accountId);
    IEnumerable<Debt> GetAllDebts();
    Debt GetDebt(int debtId);
    void AddDebt(Debt debt);
    void UpdateDebt(Debt debt);
    void DeleteDebt(int debtId);
}
