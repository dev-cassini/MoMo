using MoMo.Modules.Firms.Domain.Model;

namespace MoMo.Modules.Firms.Domain.Repositories;

internal interface IArFirmRepository
{
    internal Task AddAsync(ArFirm arFirm, CancellationToken cancellationToken);
    internal Task<ArFirm?> GetAsync(Guid id, CancellationToken cancellationToken);
}