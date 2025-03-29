using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using MoMo.Modules.LeadImporter.Application.Queries;
using MoMo.Modules.LeadImporter.Application.Services;

namespace MoMo.Api.Endpoints.LeadImporter;

public static class GetOpenApiSpecificationEndpoint
{
    public static WebApplication RegisterGetOpenApiSpecificationEndpoint(this WebApplication webApplication)
    {
        webApplication.MapGet("/lead-importer/import/open-api", Handler)
            .AllowAnonymous()
            .WithTags(nameof(LeadImporter))
            .Produces(StatusCodes.Status200OK, typeof(OpenApiDocument));

        return webApplication;
    }

    private static async Task<IResult> Handler(
        [FromServices] GetSchema.IQueryHandler getSchemaQueryHandler,
        [FromServices] IOpenApiService openApiService,
        CancellationToken cancellationToken)
    {
        var schema = await getSchemaQueryHandler.HandleAsync(cancellationToken);
        var openApiSchema = openApiService.ConvertSchema(schema);
        var openApiDocument = new OpenApiDocument
        {
            Info = new OpenApiInfo
            {
                Title = "Lead Importer",
                Description = "Import a lead."
            },
            Components = new OpenApiComponents
            {
                Schemas = new Dictionary<string, OpenApiSchema>
                {
                    { "request", openApiSchema }
                }
            },
        };
        
        return Results.Ok(openApiDocument.SerializeAsJson(OpenApiSpecVersion.OpenApi3_0));
    }
}