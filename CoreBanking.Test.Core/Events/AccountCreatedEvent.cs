// CoreBanking.Core/Events/AccountCreatedEvent.cs
using CoreBanking.Test.Core.Interfaces;
using CoreBanking.Test.Core.Entities;

namespace CoreBanking.Test.Core.Events;

public class AccountCreatedEvent : IDomainEvent
{
    public Account Account { get; }
    public DateTime OccurredOn { get; }

    public AccountCreatedEvent(Account account)
    {
        Account = account;
        OccurredOn = DateTime.UtcNow;
    }
}