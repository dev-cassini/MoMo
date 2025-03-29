using Microsoft.Extensions.DependencyInjection;
using MoMo.Modules.Firms.Domain.Model;
using MoMo.Modules.Firms.Domain.Repositories;

namespace MoMo.Modules.Firms.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddFirmInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IArFirmRepository, StubArFirmRepository>();
        return serviceCollection;
    }
}

internal class StubArFirmRepository : IArFirmRepository
{
    public Task AddAsync(ArFirm arFirm, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ArFirm?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}