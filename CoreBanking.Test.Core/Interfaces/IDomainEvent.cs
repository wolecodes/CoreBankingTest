namespace CoreBanking.Test.Core.Interfaces;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}