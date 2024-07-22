using MediatR;

namespace MoMo.Modules.LeadDistribution.Application.Recipients.Update;

internal record UpdateRecipientCommand(Guid Id, bool Enabled) : IRequest;