using CoreBanking.Test.Core.Enums;
using CoreBanking.Test.Core.ValueObjects;

namespace CoreBanking.Test.Core.Entities
{

  public class Account
  {
    public AccountId AccountId { get; private set; }
    public AccountNumber AccountNumber { get; private set; }
    public AccountType AccountType { get; private set; }
    public Money Balance { get; private set; }
    public DateTime DateOpened { get; private set; }
    public bool IsActive { get; private set; }
    public Customer Customer { get; private set; }
    public CustomerId CustomerId { get; private set; }

    //Navigation properties - private to enforce aggregate root boundaries
    private readonly List<Transaction> _transactions = new();
    public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();
     public byte[] RowVersion { get; private set; } = Array.Empty<byte>();

    public Account(AccountNumber accountNumber, AccountType accountType, CustomerId customerId)
    {
      AccountId = AccountId.Create();
      AccountNumber = accountNumber;
      AccountType = accountType;
      CustomerId = customerId;
      Balance = new Money(0);
      DateOpened = DateTime.UtcNow;

      IsActive = true;
    }

    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string? DeletedBy { get; private set; }

    public void SoftDelete(string deletedBy)
    {
        if (Balance.Amount != 0)
            throw new InvalidOperationException("Cannot close account with non-zero balance");

        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        DeletedBy = deletedBy;
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