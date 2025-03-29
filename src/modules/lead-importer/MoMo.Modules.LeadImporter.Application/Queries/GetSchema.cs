using MoMo.Modules.LeadImporter.Domain.Model;

namespace MoMo.Modules.LeadImporter.Application.Queries;

public static class GetSchema
{
    public interface IQueryHandler
    {
        Task<Schema> HandleAsync(CancellationToken cancellationToken);
    }
}