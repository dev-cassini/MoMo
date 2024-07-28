namespace MoMo.Modules.Firms.Integrations.Outbound.Notifications.Advisers;

public record AdviserCreatedIntegrationEvent(
    Guid Id,
    Guid ArFirmId);