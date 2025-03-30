using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;
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
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var schema = await getSchemaQueryHandler.HandleAsync(cancellationToken);
        var openApiRequestSchema = openApiService.ConvertSchema(schema);
        var openApiResponseSchema = new OpenApiSchema
        {
            Type = "object",
            Properties = new Dictionary<string, OpenApiSchema>
            {
                {
                    "leadId", 
                    new OpenApiSchema
                    {
                        Type = "string",
                        Format = "uuid"
                    }
                }
            }
        };
            
        var openApiDocument = new OpenApiDocument
        {
            Info = new OpenApiInfo
            {
                Title = "Lead Importer",
                Description = "Import a lead."
            },
            Servers = new List<OpenApiServer>
            {
                new()
                {
                    Url = $"https://{httpContext.Request.Host.Value}"
                }
            },
            Paths = new OpenApiPaths
            {
                ["/lead-importer/import"] = new OpenApiPathItem
                {
                    Operations = new Dictionary<OperationType, OpenApiOperation>
                    {
                        [OperationType.Post] = new()
                        {
                            Summary = "Import a lead.",
                            Responses = new OpenApiResponses
                            {
                                ["200"] = new OpenApiResponse
                                {
                                    Description = "Success",
                                    Content = new Dictionary<string, OpenApiMediaType>
                                    {
                                        ["application/json"] = new()
                                        {
                                            Schema = new OpenApiSchema
                                            {
                                                Reference = new OpenApiReference
                                                {
                                                    Type = ReferenceType.Schema,
                                                    Id = "response"
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            },
            Components = new OpenApiComponents
            {
                SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>
                {
                    {
                        "OAuth2", 
                        new OpenApiSecurityScheme
                        {
                            Type = SecuritySchemeType.OAuth2,
                            Flows = new OpenApiOAuthFlows
                            {
                                ClientCredentials = new OpenApiOAuthFlow
                                {
                                    AuthorizationUrl = new Uri("https://localhost:5001/connect/authorize"),
                                    TokenUrl = new Uri("https://localhost:5001/connect/token"),
                                    Scopes = new Dictionary<string, string>
                                    {
                                        { "lead-importer:import", "Import a lead." }
                                    }
                                }
                            }
                        }
                    }
                },
                Schemas = new Dictionary<string, OpenApiSchema>
                {
                    { "request", openApiRequestSchema },
                    { "response", openApiResponseSchema }
                }
            },
        };
        
        await using var streamWriter = new StreamWriter(httpContext.Response.Body);
        var writer = new OpenApiJsonWriter(streamWriter);
        openApiDocument.SerializeAsV3(writer);
        return Results.Empty;
    }
}