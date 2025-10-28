namespace CoreBanking.Test.Core.ValueObjects
{
  

  public record Money
  {
      public decimal Amount { get; }
      public string Currency { get; }

    public Money(decimal amount, string currency = "NGN")
    {
      if (amount < 0)
      {
        throw new ArgumentException("Money amount cannot be negative.");
      }

      if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
      {
        throw new ArgumentException("Currency must be a valid 3-letter ISO code.");
      }

      Amount = amount;
      Currency = currency;
    }
    public static Money operator +(Money a, Money b)
    {
      if (a.Currency != b.Currency)
      {
        throw new InvalidOperationException("Cannot add different currencies.");
      }
      return new Money(a.Amount + b.Amount, a.Currency);
    }
    public static Money operator -(Money a, Money b)
    {
      if (a.Currency != b.Currency)
      {
        throw new InvalidOperationException("Cannot subtract different currencies.");
      }
  
      
      return new Money(a.Amount - b.Amount, a.Currency);
    }
  }
}