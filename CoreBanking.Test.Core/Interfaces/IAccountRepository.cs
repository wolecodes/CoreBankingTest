using CoreBanking.Test.Core.ValueObjects;
using CoreBanking.Test.Core.Entities;
namespace CoreBanking.Test.Core.Interfaces
{
    public interface IAccountRepository
    {

        Task<List<Account>> GetAllAsync();
        Task<Account> GetByIdAsync(AccountId accountId);
        Task<Account> GetByAccountNumberAsync(AccountNumber accountNumber);
        Task<IEnumerable<Account>> GetByCustomerIdAsync(CustomerId customerId);
        Task AddAsync(Account account);
        Task UpdateAsync(Account account);
        Task<bool> AccountNumberExistsAsync(AccountNumber accountNumber);
    }
}