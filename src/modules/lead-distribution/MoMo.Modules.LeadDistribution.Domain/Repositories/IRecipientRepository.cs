using MoMo.Modules.LeadDistribution.Domain.Model;

namespace MoMo.Modules.LeadDistribution.Domain.Repositories;

internal interface IRecipientRepository
{
    internal Task<Recipient?> GetAsync(Guid id, CancellationToken cancellationToken);
}