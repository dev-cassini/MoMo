using Microsoft.Extensions.DependencyInjection;
using MoMo.Modules.LeadImporter.Application.Services;
using MoMo.Modules.LeadImporter.Infrastructure.Messaging.MediatR;

namespace MoMo.Modules.LeadImporter.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLeadImporterInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddServices();

        return serviceCollection;
    }
    
    private static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IImportLeadService, ImportLeadService>();

        return serviceCollection;
    }
}