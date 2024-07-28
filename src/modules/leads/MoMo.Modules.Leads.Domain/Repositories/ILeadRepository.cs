using MoMo.Modules.Leads.Domain.Model;

namespace MoMo.Modules.Leads.Domain.Repositories;

internal interface ILeadRepository
{
    Task AddAsync(Lead lead, CancellationToken cancellationToken);
}