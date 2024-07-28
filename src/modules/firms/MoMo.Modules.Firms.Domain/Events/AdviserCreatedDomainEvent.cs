using MediatR;

namespace MoMo.Modules.Firms.Domain.Events;

internal record AdviserCreatedDomainEvent(
    Guid Id,
    Guid ArFirmId) : INotification;