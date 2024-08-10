using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace MoMo.Modules.LeadImporter.IntegrationTests;

[SetUpFixture]
public class WebApplicationSetUpFixture
{
    private static WebApplicationFactory<Program> _factory = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                var configuration = new ConfigurationBuilder()
                    .AddInMemoryCollection(new List<KeyValuePair<string, string?>>())
                    .Build();

                builder
                    .UseEnvironment("Test")
                    .UseConfiguration(configuration)
                    .ConfigureTestServices(collection =>
                    {
                    });
            });
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _factory.Dispose();
    }

    public static HttpClient CreateHttpClient()
    {
        return _factory.CreateClient();
    }
}