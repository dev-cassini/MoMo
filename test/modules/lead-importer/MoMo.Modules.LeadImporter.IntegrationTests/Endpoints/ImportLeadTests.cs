using System.Net.Http.Json;
using MoMo.Modules.LeadImporter.Application.Commands;
using Newtonsoft.Json.Linq;

namespace MoMo.Modules.LeadImporter.IntegrationTests.Endpoints;

[TestFixture]
public class ImportLeadTests : TestBase
{
    private RequestDelegate CreateRequestDelegate(ImportLeadCommand command)
        => httpClient => httpClient.PostAsJsonAsync("/lead-importer/import", command);
    
    [Test]
    public async Task WhenRequestIsValid_ThenResponseIs200Ok()
    {
        var command = new ImportLeadCommand(new JObject());

        var (response, content) = await CallApi<ImportLeadResponse>(CreateRequestDelegate(command));
    }
}