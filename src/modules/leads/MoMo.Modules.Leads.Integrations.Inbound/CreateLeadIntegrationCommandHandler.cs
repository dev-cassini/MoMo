using MediatR;
using MoMo.Modules.Leads.Application.Commands.Create;
using MoMo.Modules.Leads.Integrations.Inbound.Notifications;
using LeadCustomerDto = MoMo.Modules.Leads.Application.Commands.Create.LeadCustomerDto;

namespace MoMo.Modules.Leads.Integrations.Inbound;

public class CreateLeadIntegrationCommandHandler(IMediator mediator) : IRequestHandler<CreateLeadIntegrationEvent, Guid>
{
    public async Task<Guid> Handle(CreateLeadIntegrationEvent request, CancellationToken cancellationToken)
    {
        var command = new CreateLeadCommand(
            request.AdviserId,
            request.LeadCustomers.Select(x => new LeadCustomerDto(
                x.FirstName,
                x.LastName,
                x.EmailAddress)));

        var leadId = await mediator.Send(command, cancellationToken);

        return leadId;
    }
}