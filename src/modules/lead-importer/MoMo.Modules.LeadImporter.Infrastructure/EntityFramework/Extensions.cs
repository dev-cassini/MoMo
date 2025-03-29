using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoMo.Modules.LeadImporter.Application.Queries;
using MoMo.Modules.LeadImporter.Domain.Repositories;
using MoMo.Modules.LeadImporter.Infrastructure.EntityFramework.Queries;
using MoMo.Modules.LeadImporter.Infrastructure.EntityFramework.Repositories;

namespace MoMo.Modules.LeadImporter.Infrastructure.EntityFramework;

internal static class Extensions
{
    internal static IServiceCollection AddEntityFramework(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection
            .AddDbContext<LeadImporterDbContext>(builder =>
            {
                var connectionString = configuration.GetConnectionString("Postgres");
                builder.UseNpgsql(connectionString, optionsBuilder =>
                {
                    optionsBuilder.MigrationsHistoryTable("__EFMigrationsHistory", LeadImporterDbContext.Schema);
                });
            })
            .AddEfRepositories()
            .AddEfQueries();

        return serviceCollection;
    }

    internal static IServiceProvider MigrateDatabase(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<LeadImporterDbContext>();
        dbContext.Database.Migrate();

        return serviceProvider;
    }

    private static IServiceCollection AddEfRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<ISchemaRepository, EfSchemaRepository>();

        return serviceCollection;
    }

    private static IServiceCollection AddEfQueries(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<GetSchema.IQueryHandler, EfGetSchema>();

        return serviceCollection;
    }
}