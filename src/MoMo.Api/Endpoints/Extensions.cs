using MoMo.Api.Endpoints.LeadImporter;

namespace MoMo.Api.Endpoints;

public static class Extensions
{
    public static WebApplication RegisterEndpoints(this WebApplication webApplication)
    {
        webApplication
            .RegisterImportLeadEndpoint();

        return webApplication;
    }
}