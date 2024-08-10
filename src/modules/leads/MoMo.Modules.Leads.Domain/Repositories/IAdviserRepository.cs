using MoMo.Modules.Leads.Domain.Model;

namespace MoMo.Modules.Leads.Domain.Repositories;

public interface IAdviserRepository
{
    Task<Adviser?> GetAsync(Guid id, CancellationToken cancellationToken);
}