using MediatR;

namespace MoMo.Modules.LeadDistribution.Domain.Events.Recipients;

internal record RecipientUpdatedDomainEvent(Guid Id, bool Enabled) : INotification;