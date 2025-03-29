using Microsoft.EntityFrameworkCore;
using MoMo.Modules.LeadImporter.Application.Queries;
using MoMo.Modules.LeadImporter.Domain.Model;

namespace MoMo.Modules.LeadImporter.Infrastructure.EntityFramework.Queries;

public class EfGetSchema(LeadImporterDbContext dbContext) : GetSchema.IQueryHandler
{
    public async Task<Schema> HandleAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Schemas.SingleAsync(cancellationToken);
    }
}