using CoreBanking.Test.Core.Entities;
namespace CoreBanking.Test.Core.Interfaces
{
  public interface ITransactionRepository
  {
    // Define methods for transaction repository
    Task<IEnumerable<Transaction>> GetByAccountIdAsync(Guid accountId);
    Task AddAsync(Transaction transaction);
  }
}