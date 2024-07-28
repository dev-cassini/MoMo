using MediatR;
using MoMo.BuildingBlocks.Application.Exceptions;
using MoMo.Modules.Leads.Domain.Model;
using MoMo.Modules.Leads.Domain.Repositories;

namespace MoMo.Modules.Leads.Application.Commands.Create;

internal class CreateLeadCommandHandler(
    ILeadRepository leadRepository,
    IAdviserRepository adviserRepository) : IRequestHandler<CreateLeadCommand, Guid>
{
    public async Task<Guid> Handle(CreateLeadCommand request, CancellationToken cancellationToken)
    {
        var adviser = await adviserRepository.GetAsync(request.AdviserId, cancellationToken);
        if (adviser is null)
        {
            throw new BadRequestExceptions.NotFoundException(request.AdviserId, typeof(Adviser));
        }
        
        var lead = request.ToLead(adviser);
        await leadRepository.AddAsync(lead, cancellationToken);

        return lead.Id;
    }
}