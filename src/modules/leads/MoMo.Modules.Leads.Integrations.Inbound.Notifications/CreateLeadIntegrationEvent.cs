using MediatR;

namespace MoMo.Modules.Leads.Integrations.Inbound.Notifications;
    
public record CreateLeadIntegrationEvent(
    Guid AdviserId,
    IEnumerable<LeadCustomerDto> LeadCustomers) : IRequest<Guid>;

public record LeadCustomerDto(
    string FirstName,
    string LastName,
    string EmailAddress);