using Microsoft.Extensions.DependencyInjection;
using MoMo.Modules.Leads.Domain.Repositories;
using Moq;

namespace MoMo.Modules.Leads.Application.UnitTests;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddMockedRepositories(this IServiceCollection serviceCollection)
    {
        var leadRepository = new Mock<ILeadRepository>();
        serviceCollection
            .AddSingleton(leadRepository)
            .AddSingleton(leadRepository.Object);
        
        return serviceCollection;
    }
}