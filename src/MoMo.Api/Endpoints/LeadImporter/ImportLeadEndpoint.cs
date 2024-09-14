using System.Text.Json;
using System.Text.Json.Nodes;
using MediatR;
using MoMo.Modules.LeadImporter.Application.Commands;

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
        JsonNode request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new ImportLeadCommand(request);
        var response = await mediator.Send(command, cancellationToken);
        return Results.Ok(response);
    }
}