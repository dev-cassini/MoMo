using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoMo.Modules.LeadImporter.Application.Services;
using MoMo.Modules.LeadImporter.Infrastructure.EntityFramework;
using MoMo.Modules.LeadImporter.Infrastructure.JsonSchemaDotNet;
using MoMo.Modules.LeadImporter.Infrastructure.Messaging.MediatR;

namespace MoMo.Modules.LeadImporter.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddLeadImporterInfrastructure(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection
            .AddEntityFramework(configuration)
            .AddServices();

        return serviceCollection;
    }
    
    private static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IImportLeadService, ImportLeadService>()
            .AddSingleton<IJsonValidationService, JsonSchemaDotNetValidationService>()
            .AddSingleton<IOpenApiService, JsonSchemaDotNetOpenApiService>();

        return serviceCollection;
    }
    
    public static IServiceProvider UseLeadImporterInfrastructure(this IServiceProvider serviceProvider)
    {
        serviceProvider.MigrateDatabase();

        return serviceProvider;
    }
}