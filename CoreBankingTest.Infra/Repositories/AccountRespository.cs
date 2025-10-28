// using CoreBanking.Test.Core.Interfaces;
// using CoreBanking.Test.Core.Models;

// namespace CoreBankingTest.Infra.Repositories
// {
//     public class AccountRepository : IAccountRepository
//     {
//     private readonly List<AccountModel> _accounts = new()
//     {
//         new AccountModel { Id = 1, Name = "John Doe", Balance = 1000 },
//         new AccountModel { Id = 2, Name = "John smith", Balance = 1500 },
//     };
//         public AccountModel? GetAccountById(int id)
//         {
//             return _accounts.FirstOrDefault(a => a.Id == id);
//         }

//         public IEnumerable<AccountModel> GetAllAccounts()
//         {
//             return _accounts;
//         }

//         public void Add(AccountModel account)
//         {
//             _accounts.Add(account);
//         }

//         // Interface async members implemented (simple stubs) to satisfy IAccountRepository
//         public Task<Account> GetByIdAsync(Guid id)
//         {
//             throw new NotImplementedException();
//         }

//         public Task<Account> GetByAccountNumberAsync(AccountNumber accountNumber)
//         {
//             throw new NotImplementedException();
//         }

//         public Task<IEnumerable<Account>> GetByCustomerIdAsync(Guid customerId)
//         {
//             throw new NotImplementedException();
//         }

//         public Task<IEnumerable<Account>> GetAllAccountsAsync()
//         {
//             throw new NotImplementedException();
//         }

//         public Task<bool> AccountNumberExistsAsync(AccountNumber accountNumber)
//         {
//             throw new NotImplementedException();
//         }

//         public Task AddAsync(Account account)
//         {
//             throw new NotImplementedException();
//         }

//         public Task UpdateAsync(Account account)
//         {
//             throw new NotImplementedException();
//         }
//     }
// }