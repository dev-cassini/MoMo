using MoMo.Modules.Leads.Domain.Model;
using MoMo.Modules.Leads.Domain.Repositories;

namespace MoMo.Modules.Leads.Infrastructure.Persistence.EntityFramework.Repositories;

public class EfLeadRepository : ILeadRepository
{
    public async Task AddAsync(Lead lead, CancellationToken cancellationToken)
    {
    }
}