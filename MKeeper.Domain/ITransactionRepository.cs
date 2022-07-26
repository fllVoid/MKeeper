using MKeeper.Domain.Models;

namespace MKeeper.Domain;

    public interface ITransactionRepository
{
	Transaction GetTransaction(int transactionId);
	IEnumerable<Transaction> GetTransactions(
		int[] accountIds,
		int[] categoryIds,
		DateTime from,
		DateTime to);
	IEnumerable<Transaction> GetTransactions(int[] accountIds, int[] categoryIds);
	IEnumerable<Transaction> GetTransactions(int[] accountIds);
	IEnumerable<Transaction> GetTransactions(int accountId);
	IEnumerable<Transaction> GetTransactions(int accountId,
		int[] categoryIds,
		DateTime from,
		DateTime to);
	IEnumerable<Transaction> GetTransactions(int accountId, int[] categoryIds);
	IEnumerable<Transaction> GetAllTransactions();
	void AddTransaction(Transaction transaction);
	void UpdateTransaction(Transaction transaction);
	void DeleteTransaction(int transactionId);
}
