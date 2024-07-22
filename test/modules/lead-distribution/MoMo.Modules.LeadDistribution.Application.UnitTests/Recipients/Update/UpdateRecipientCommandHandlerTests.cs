using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MoMo.BuildingBlocks.Application.Exceptions;
using MoMo.Modules.LeadDistribution.Application.Recipients.Update;
using MoMo.Modules.LeadDistribution.Builders;
using MoMo.Modules.LeadDistribution.Domain.Model;
using MoMo.Modules.LeadDistribution.Domain.Repositories;
using Moq;

namespace MoMo.Modules.LeadDistribution.Application.UnitTests.Recipients.Update;

[TestFixture]
public class UpdateRecipientCommandHandlerTests
{
    private ServiceProvider _serviceProvider;
    
    [SetUp]
    public void SetUp()
    {
        _serviceProvider = new ServiceCollection()
            .AddMockedRepositories()
            .AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(Marker).Assembly);
            })
            .BuildServiceProvider();
    }

    [TearDown]
    public void TearDown()
    {
        _serviceProvider.Dispose();
    }
    
    [Test]
    public void WhenRecipientDoesNotExist_ThenExceptionIsThrown()
    {
        // Arrange
        var request = new UpdateRecipientCommand(Guid.NewGuid(), true);
        
        var recipientRepository = _serviceProvider.GetRequiredService<Mock<IRecipientRepository>>();
        recipientRepository.Setup(x => x.GetAsync(request.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Recipient?)null);

        // Act & Assert
        var exception = Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            var mediator = _serviceProvider.GetRequiredService<IMediator>();
            await mediator.Send(request);
        });
        
        Assert.That(exception!.Message, Is.EqualTo($"Recipient with id {request.Id} could not be found."));
    }
    
    [Test]
    public async Task WhenRecipientDoesExist_ThenRecipientIsUpdated()
    {
        // Arrange
        var recipient = new RecipientBuilder()
            .WithEnabled(false)
            .Build();
        
        var request = new UpdateRecipientCommand(recipient.Id, true);
        
        var recipientRepository = _serviceProvider.GetRequiredService<Mock<IRecipientRepository>>();
        recipientRepository.Setup(x => x.GetAsync(request.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(recipient);

        // Act
        var mediator = _serviceProvider.GetRequiredService<IMediator>();
        await mediator.Send(request);
        
        // Assert
        Assert.That(recipient.Enabled, Is.True);
    }
}