using System.Text.Json;
using MediatR;
using MoMo.Modules.LeadImporter.Application.Commands;
using Newtonsoft.Json.Linq;

namespace MoMo.Api.Endpoints.LeadImporter;

public static class ImportLeadEndpoint
{
    public static WebApplication RegisterImportLeadEndpoint(this WebApplication webApplication)
    {
        webApplication.MapPost("/lead-importer/import", Handler)
            .Accepts<ImportLeadCommand>("application/json")
            .AllowAnonymous()
            .WithTags(nameof(LeadImporter))
            .Produces(StatusCodes.Status200OK, typeof(ImportLeadResponse));

        return webApplication;
    }
    
    /// <summary>
    /// Import a lead.
    /// </summary>
    /// <param name="request">Request.</param>
    /// <param name="mediator">Mediator.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    private static async Task<IResult> Handler(
        JsonElement request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        // var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        // var command = request.Deserialize<ImportLeadCommand>(options);
        
        var jObject = JObject.Parse(request.GetRawText());
        var command = new ImportLeadCommand(jObject);
        var response = await mediator.Send(command, cancellationToken);
        return Results.Ok(response);
    }
}