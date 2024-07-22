using MoMo.BuildingBlocks.Domain;
using MoMo.Modules.LeadDistribution.Domain.Events.Recipients;

namespace MoMo.Modules.LeadDistribution.Domain.Model;

internal class Recipient(Guid id, string name, bool enabled) : AggregateRoot
{
    internal Guid Id { get; } = id;
    internal string Name { get; } = name;
    internal bool Enabled { get; private set; } = enabled;

    internal void Update(bool enabled)
    {
        Enabled = enabled;
        AddDomainEvent(new RecipientUpdatedDomainEvent(Id, Enabled));
    }
}