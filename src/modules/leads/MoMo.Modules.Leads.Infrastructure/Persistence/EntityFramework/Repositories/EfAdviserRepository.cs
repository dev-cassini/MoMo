using MoMo.Modules.Leads.Domain.Model;
using MoMo.Modules.Leads.Domain.Repositories;

namespace MoMo.Modules.Leads.Infrastructure.Persistence.EntityFramework.Repositories;

public class EfAdviserRepository : IAdviserRepository
{
    public async Task<Adviser?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return null;
    }
}