using CoreBanking.Test.Core.Enums;
using CoreBanking.Test.Core.ValueObjects;

namespace CoreBanking.Test.Core.Entities
{
  public class Transaction
  {
    public Guid TransactionId { get; private set; }
    public Guid AccountId { get; private set; }
    public TransactionType Type { get; private set; }
    public Money Amount { get; private set; }
    public string Description { get; private set; }
    public DateTime TimeStamp { get; private set; }
    public string Reference { get; private set; }


    // Required For EF Core
    private Transaction() { }


    public Transaction(Guid accountId, Money amount, TransactionType type, string description = "")
    {
      TransactionId = Guid.NewGuid();
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