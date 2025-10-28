using CoreBanking.Test.Core.Enums;
using CoreBanking.Test.Core.ValueObjects;
using CoreBanking.Test.Core.Entities;
namespace CoreBanking.Test.Core
{

  public class Account
  {
    public Guid AccountId { get; private set; }
    public AccountNumber AccountNumber { get; private set; }
    public AccountType AccountType { get; private set; }
    public Money Balance { get; private set; }
    public DateTime DateOpened { get; private set; }
    public bool IsActive { get; private set; }
    public Guid CustomerId { get; private set; }

    //Navigation properties - private to enforce aggregate root boundaries
    private readonly List<Transaction> _transactions = new();
    public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

    public Account(AccountNumber accountNumber, AccountType accountType, Guid customerId)
    {
      AccountId = Guid.NewGuid();
      AccountNumber = accountNumber;
      AccountType = accountType;
      CustomerId = customerId;
      Balance = new Money(0);
      DateOpened = DateTime.UtcNow;

      IsActive = true;
      CustomerId = customerId;
    }

    //Business Methods

    public Transaction Deposit(Money amount, string description = "Deposit")
    {
      if (!IsActive)
        throw new InvalidOperationException("Cannot deposit to an inactive account.");


      if (amount.Amount <= 0)
        throw new ArgumentException("Deposit amount must be greater than zero (positive).");

      Balance += amount;

      var transaction = new Transaction(accountId: AccountId, amount: amount, type: TransactionType.Deposit, description: description);
      _transactions.Add(transaction);

      return transaction;
    }

    public Transaction Withdraw(Money amount, string description = "Withdrawal")
    {
      if (!IsActive)
        throw new InvalidOperationException("Cannot withdraw from an inactive account.");

      if (amount.Amount <= 0)
        throw new ArgumentException("Withdrawal amount must be greater than zero (positive).");

      if (Balance.Amount < amount.Amount)
        throw new InvalidOperationException("Insufficient funds");

      if (AccountType == AccountType.Savings && (Balance.Amount - amount.Amount) >= 6)
        throw new InvalidOperationException("Savings account withdrawal limit reached");

      Balance -= amount;

      var transaction = new Transaction(accountId: AccountId, amount: amount, type: TransactionType.Withdrawal, description: description);
      _transactions.Add(transaction);

      return transaction;
    }

    public void CloseAccount()
    {
      if (Balance.Amount > 0)
        throw new InvalidOperationException("Cannot deactivate account with remaining balance.");

      IsActive = false;
    }
  }
}