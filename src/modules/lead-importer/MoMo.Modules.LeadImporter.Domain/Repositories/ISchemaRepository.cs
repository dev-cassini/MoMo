using MoMo.Modules.LeadImporter.Domain.Model;

namespace MoMo.Modules.LeadImporter.Domain.Repositories;

public interface ISchemaRepository
{
    Task AddAsync(Schema schema, CancellationToken cancellationToken);
    Task<Schema> GetSchemaAsync(CancellationToken cancellationToken);
}