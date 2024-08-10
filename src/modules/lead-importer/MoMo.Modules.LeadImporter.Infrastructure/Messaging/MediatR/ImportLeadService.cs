using MediatR;
using MoMo.Modules.LeadImporter.Application.Services;
using MoMo.Modules.Leads.Integrations.Inbound.Notifications;

namespace MoMo.Modules.LeadImporter.Infrastructure.Messaging.MediatR;

public class ImportLeadService(IMediator mediator) : IImportLeadService
{
    public async Task<Guid> ExecuteAsync(
        IImportLeadService.ImportLeadRequest importLeadRequest, 
        CancellationToken cancellationToken)
    {
        var integrationCommand = new CreateLeadIntegrationEvent(
            importLeadRequest.AdviserId,
            importLeadRequest.LeadCustomers.Select(x => new LeadCustomerDto(
                x.FirstName,
                x.LastName,
                x.EmailAddress)));
        
        var leadId = await mediator.Send(integrationCommand, cancellationToken);
        return leadId;
    }
}