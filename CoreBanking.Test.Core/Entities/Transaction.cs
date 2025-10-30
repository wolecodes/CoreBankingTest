using CoreBanking.Test.Core.Enums;
using CoreBanking.Test.Core.ValueObjects;

namespace CoreBanking.Test.Core.Entities
{
  public class Transaction
  {
    public TransactionId TransactionId { get; private set; }
    public AccountId AccountId { get; private set; }
    public Account Account { get; private set; }
    public TransactionType Type { get; private set; }
    public Money Amount { get; private set; }
    public string Description { get; private set; }
    public DateTime TimeStamp { get; private set; }
  
    public string Reference { get; private set; }


    // Required For EF Core
    private Transaction() { }


    public Transaction(AccountId accountId, Money amount, TransactionType type, string description = "")
    {
      TransactionId = TransactionId.Create();
      AccountId = accountId;
      Type = type;
      Amount = amount;
      Description = description ?? throw new ArgumentNullException(nameof(description));
      TimeStamp = DateTime.UtcNow;
      Reference = GenerateReference();
    } 

    private string GenerateReference()
    {
      return $"{TimeStamp:yyyyMMddHHmmss}-{TransactionId.ToString().Substring(0, 8)}";
    }
  
  }
}