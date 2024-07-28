using MediatR;
using MoMo.Modules.Leads.Domain.Repositories;

namespace MoMo.Modules.Leads.Application.Commands.Create;

internal class CreateLeadCommandHandler(
    ILeadRepository leadRepository) : IRequestHandler<CreateLeadCommand, Guid>
{
    public async Task<Guid> Handle(CreateLeadCommand request, CancellationToken cancellationToken)
    {
        var lead = request.ToLead();
        await leadRepository.AddAsync(lead, cancellationToken);

        return lead.Id;
    }
}