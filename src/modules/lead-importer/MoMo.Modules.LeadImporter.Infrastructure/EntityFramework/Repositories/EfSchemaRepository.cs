using Microsoft.EntityFrameworkCore;
using MoMo.Modules.LeadImporter.Domain.Model;
using MoMo.Modules.LeadImporter.Domain.Repositories;

namespace MoMo.Modules.LeadImporter.Infrastructure.EntityFramework.Repositories;

public class EfSchemaRepository(LeadImporterDbContext dbContext) : ISchemaRepository
{
    public async Task AddAsync(Schema schema, CancellationToken cancellationToken)
    {
        await dbContext.Schemas.AddAsync(schema, cancellationToken);
    }

    public async Task<Schema> GetAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Schemas.SingleAsync(cancellationToken);
    }
}