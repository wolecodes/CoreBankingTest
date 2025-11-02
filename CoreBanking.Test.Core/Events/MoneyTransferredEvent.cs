// CoreBanking.Core/Events/MoneyTransferredEvent.cs
using CoreBanking.Test.Core.Interfaces;
using CoreBanking.Test.Core.Entities;
using CoreBanking.Test.Core.ValueObjects;

namespace CoreBanking.Test.Core.Events;

public class MoneyTransferredEvent : IDomainEvent
{
    public Account SourceAccount { get; }
    public Account DestinationAccount { get; }
    public Money Amount { get; }
    public string Reference { get; }
    public DateTime OccurredOn { get; }

    public MoneyTransferredEvent(Account sourceAccount, Account destinationAccount, Money amount, string reference)
    {
        SourceAccount = sourceAccount;
        DestinationAccount = destinationAccount;
        Amount = amount;
        Reference = reference;
        OccurredOn = DateTime.UtcNow;
    }
}