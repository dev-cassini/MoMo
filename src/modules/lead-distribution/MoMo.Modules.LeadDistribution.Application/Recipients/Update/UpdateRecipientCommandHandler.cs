using MediatR;
using MoMo.BuildingBlocks.Application.Exceptions;
using MoMo.Modules.LeadDistribution.Domain.Model;
using MoMo.Modules.LeadDistribution.Domain.Repositories;

namespace MoMo.Modules.LeadDistribution.Application.Recipients.Update;

internal class UpdateRecipientCommandHandler(
    IRecipientRepository recipientRepository,
    IPublisher publisher) : IRequestHandler<UpdateRecipientCommand>
{
    public async Task Handle(UpdateRecipientCommand request, CancellationToken cancellationToken)
    {
        var recipient = await recipientRepository.GetAsync(request.Id, cancellationToken);
        if (recipient is null)
        {
            throw new NotFoundException(request.Id, typeof(Recipient));
        }
        
        recipient.Update(request.Enabled);
        foreach (var domainEvent in recipient.DomainEvents)
        {
            await publisher.Publish(domainEvent, cancellationToken);
        }
    }
}