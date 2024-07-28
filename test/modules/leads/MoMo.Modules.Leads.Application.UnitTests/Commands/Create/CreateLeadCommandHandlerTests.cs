using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MoMo.BuildingBlocks.Application.Exceptions;
using MoMo.Modules.Leads.Application.Commands.Create;
using MoMo.Modules.Leads.Domain.Model;
using MoMo.Modules.Leads.Domain.Repositories;
using Moq;

namespace MoMo.Modules.Leads.Application.UnitTests.Commands.Create;

[TestFixture]
public class CreateLeadCommandHandlerTests
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
    public void WhenAdviserDoesNotExist_ThenBadRequestExceptionIsThrown()
    {
        // Arrange
        var command = new CreateLeadCommand(
            Guid.NewGuid(),
            new List<LeadCustomerDto>
            {
                new("Customer", "One", "customer.one@gmail.com")
            });
        
        var adviserRepository = _serviceProvider.GetRequiredService<Mock<IAdviserRepository>>();
        adviserRepository.Setup(x => x.GetAsync(command.AdviserId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Adviser?)null);
        
        // Act & Assert
        Assert.ThrowsAsync<BadRequestExceptions.NotFoundException>(async () =>
        {
            var mediator = _serviceProvider.GetRequiredService<IMediator>();
            await mediator.Send(command);
        });
    }

    [Test]
    public async Task OneLeadIsAddedToRepository()
    {
        // Arrange
        var adviser = new Adviser(Guid.NewGuid(), Guid.NewGuid());
        var command = new CreateLeadCommand(
            adviser.Id,
            new List<LeadCustomerDto>
            {
                new("Customer", "One", "customer.one@gmail.com")
            });
        
        var adviserRepository = _serviceProvider.GetRequiredService<Mock<IAdviserRepository>>();
        adviserRepository.Setup(x => x.GetAsync(command.AdviserId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(adviser);
        
        // Act
        var mediator = _serviceProvider.GetRequiredService<IMediator>();
        await mediator.Send(command);
        
        // Assert
        var leadRepository = _serviceProvider.GetRequiredService<Mock<ILeadRepository>>();
        leadRepository.Verify(x => x.AddAsync(
            It.IsAny<Lead>(),
            It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Test]
    public async Task CommandIsMappedToLead()
    {
        // Arrange
        var adviser = new Adviser(Guid.NewGuid(), Guid.NewGuid());
        var command = new CreateLeadCommand(
            adviser.Id,
            new List<LeadCustomerDto>
            {
                new("Bob", "Belcher", "bob.belcher@gmail.com"),
            });
        
        var adviserRepository = _serviceProvider.GetRequiredService<Mock<IAdviserRepository>>();
        adviserRepository.Setup(x => x.GetAsync(command.AdviserId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(adviser);
        
        // Act & Assert
        var leadRepository = _serviceProvider.GetRequiredService<Mock<ILeadRepository>>();
        leadRepository.Setup(x => x.AddAsync(
                It.IsAny<Lead>(),
                It.IsAny<CancellationToken>()))
            .Callback((Lead lead, CancellationToken _) =>
            {
                Assert.That(lead.AdviserId, Is.EqualTo(command.AdviserId));
            });
        
        var mediator = _serviceProvider.GetRequiredService<IMediator>();
        await mediator.Send(command);
    }
    
    [Test]
    public async Task CommandIsMappedToLeadCustomers()
    {
        // Arrange
        var adviser = new Adviser(Guid.NewGuid(), Guid.NewGuid());
        var command = new CreateLeadCommand(
            adviser.Id,
            new List<LeadCustomerDto>
            {
                new("Bob", "Belcher", "bob.belcher@gmail.com"),
                new("Linda", "Belcher", "linda.belcher@gmail.com")
            });
        
        var adviserRepository = _serviceProvider.GetRequiredService<Mock<IAdviserRepository>>();
        adviserRepository.Setup(x => x.GetAsync(command.AdviserId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(adviser);
        
        // Act & Assert
        var leadRepository = _serviceProvider.GetRequiredService<Mock<ILeadRepository>>();
        leadRepository.Setup(x => x.AddAsync(
                It.IsAny<Lead>(),
                It.IsAny<CancellationToken>()))
            .Callback((Lead lead, CancellationToken _) =>
            {
                Assert.That(lead.Customers, Has.Count.EqualTo(2));
                Assert.Multiple(() =>
                {
                    var leadCustomerDtos = command.LeadCustomers.ToList();
                    Assert.That(lead.Customers[0].FirstName, Is.EqualTo(leadCustomerDtos[0].FirstName));
                    Assert.That(lead.Customers[0].LastName, Is.EqualTo(leadCustomerDtos[0].LastName));
                    Assert.That(lead.Customers[0].EmailAddress, Is.EqualTo(leadCustomerDtos[0].EmailAddress));
                    
                    Assert.That(lead.Customers[1].FirstName, Is.EqualTo(leadCustomerDtos[1].FirstName));
                    Assert.That(lead.Customers[1].LastName, Is.EqualTo(leadCustomerDtos[1].LastName));
                    Assert.That(lead.Customers[1].EmailAddress, Is.EqualTo(leadCustomerDtos[1].EmailAddress));
                });
            });
        
        var mediator = _serviceProvider.GetRequiredService<IMediator>();
        await mediator.Send(command);
    }
}