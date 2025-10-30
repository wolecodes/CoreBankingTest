using CoreBanking.Test.Core.Interfaces;
using CoreBankingTest.Infra.Data;
using CoreBanking.Test.Core.Entities;
using CoreBanking.Test.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
namespace CoreBanking.Infra.Repositories
{
  
}
    public class AccountRepository : IAccountRepository
    {
        private readonly BankingDbContext _context;

        public AccountRepository(BankingDbContext context)
        {
            _context = context;
        }



  public async Task<List<Account>> GetAllAsync()
  {
    return await _context.Accounts
        .Include(a => a.Transactions)
        .ToListAsync();
  }
  public async Task<Account> GetByIdAsync(AccountId accountId)
  {
    return await _context.Accounts
        .Include(a => a.Transactions)
        .FirstOrDefaultAsync(a => a.AccountId == accountId);
  }

  public async Task<Account> GetByAccountNumberAsync(AccountNumber accountNumber)
  {
    return await _context.Accounts
        .Include(a => a.Transactions)
        .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
  }

  public async Task<IEnumerable<Account>> GetByCustomerIdAsync(CustomerId customerId)
  {
      return await _context.Accounts
          .Where(a => a.CustomerId == customerId)
          .Include(a => a.Transactions)
                .ToListAsync();
        }

        public async Task AddAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
        }

        public async Task UpdateAsync(Account account)
        {
            _context.Accounts.Update(account);
            await Task.CompletedTask;
        }

        public async Task<bool> AccountNumberExistsAsync(AccountNumber accountNumber)
        {
            return await _context.Accounts
                .AnyAsync(a => a.AccountNumber == accountNumber);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
}
