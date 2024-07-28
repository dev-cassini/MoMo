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
        
        var adviserRepository = new Mock<IAdviserRepository>();
        serviceCollection
            .AddSingleton(adviserRepository)
            .AddSingleton(adviserRepository.Object);
        
        return serviceCollection;
    }
}