using CoreBanking.Test.Core.Entities;
using CoreBanking.Test.Core.ValueObjects;
namespace CoreBanking.Test.Core.Interfaces
{
  public interface ITransactionRepository
  {
    // Define methods for transaction repository  
Task<IEnumerable<Transaction>> GetByAccountIdAsync(AccountId accountId, CancellationToken cancellationToken = default);
    Task<Transaction?> GetByIdAsync(TransactionId transactionId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Transaction>> GetByAccountIdAndDateRangeAsync(AccountId accountId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default);
    Task UpdateAsync(Transaction transaction, CancellationToken cancellationToken = default);
  }
}

