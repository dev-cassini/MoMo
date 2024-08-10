using Microsoft.Extensions.DependencyInjection;
using MoMo.Modules.Leads.Domain.Repositories;
using MoMo.Modules.Leads.Infrastructure.Persistence.EntityFramework.Repositories;

namespace MoMo.Modules.Leads.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLeadInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddEfRepositories();

        return serviceCollection;
    }

    private static IServiceCollection AddEfRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IAdviserRepository, EfAdviserRepository>()
            .AddScoped<ILeadRepository, EfLeadRepository>();

        return serviceCollection;
    }
}