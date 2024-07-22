using MediatR;

namespace MoMo.BuildingBlocks.Domain;

public abstract class AggregateRoot
{
    private readonly List<INotification> _domainEvents = [];
    public IReadOnlyList<INotification> DomainEvents => _domainEvents;

    protected void AddDomainEvent(INotification eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(INotification @event)
    {
        _domainEvents.Remove(@event);
    }
}