using Microsoft.Extensions.DependencyInjection;
using MoMo.Modules.LeadDistribution.Domain.Repositories;
using Moq;

namespace MoMo.Modules.LeadDistribution.Application.UnitTests;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddMockedRepositories(this IServiceCollection serviceCollection)
    {
        var recipientRepository = new Mock<IRecipientRepository>();
        serviceCollection
            .AddSingleton(recipientRepository)
            .AddSingleton(recipientRepository.Object);
        
        return serviceCollection;
    }
}