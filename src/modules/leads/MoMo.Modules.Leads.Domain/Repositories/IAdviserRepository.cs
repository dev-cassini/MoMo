using MoMo.Modules.Leads.Domain.Model;

namespace MoMo.Modules.Leads.Domain.Repositories;

internal interface IAdviserRepository
{
    internal Task<Adviser?> GetAsync(Guid id, CancellationToken cancellationToken);
}