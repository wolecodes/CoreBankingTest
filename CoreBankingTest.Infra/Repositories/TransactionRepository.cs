using CoreBanking.Test.Core.Entities;
using CoreBanking.Test.Core.Interfaces;
using CoreBanking.Test.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using CoreBankingTest.Infra.Data;
namespace CoreBankingTest.Infra.Repositories
{
  public class TransactionRepository : ITransactionRepository
  {
    private readonly BankingDbContext _context;

    public TransactionRepository(BankingDbContext context)
    {
      _context = context;
    }

    public async Task<Transaction?> GetByIdAsync(TransactionId transactionId, CancellationToken cancellationToken = default)
    {
      return await _context.Transactions
        .FirstOrDefaultAsync(t => t.TransactionId == transactionId, cancellationToken);
    }

    public async Task<IEnumerable<Transaction>> GetByAccountIdAsync(AccountId accountId, CancellationToken cancellationToken = default)
    {
      return await _context.Transactions
        .Where(t => t.AccountId == accountId)
        .OrderByDescending(t => t.TimeStamp)
        .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Transaction>> GetByAccountIdAndDateRangeAsync(AccountId accountId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
      return await _context.Transactions
        .Where(t => t.AccountId == accountId &&
              t.TimeStamp >= startDate &&
              t.TimeStamp <= endDate)
        .OrderBy(t => t.TimeStamp)
        .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default)
    {
      await _context.Transactions.AddAsync(transaction, cancellationToken);
    }

    public async Task UpdateAsync(Transaction transaction, CancellationToken cancellationToken = default)
    {
      _context.Transactions.Update(transaction);
      await Task.CompletedTask;
    }
  }
}