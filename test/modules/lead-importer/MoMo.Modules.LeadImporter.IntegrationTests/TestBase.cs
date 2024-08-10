using System.Net.Http.Json;
using System.Text.Json;

namespace MoMo.Modules.LeadImporter.IntegrationTests;

[TestFixture]
public class TestBase
{
    private HttpClient _httpClient;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _httpClient = WebApplicationSetUpFixture.CreateHttpClient();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _httpClient.Dispose();
    }

    protected delegate Task<HttpResponseMessage> RequestDelegate(HttpClient httpClient);

    protected async Task<(HttpResponseMessage Response, TContent? Content)> CallApi<TContent>(RequestDelegate requestDelegate)
        where TContent : class
    {
        var response = await requestDelegate(_httpClient);
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        var content = await response.Content.ReadFromJsonAsync<TContent>(options);

        return (response, content);
    }
}