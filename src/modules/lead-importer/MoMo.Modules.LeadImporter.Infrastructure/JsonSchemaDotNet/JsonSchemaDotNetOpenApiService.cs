using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using MoMo.Modules.LeadImporter.Application.Services;
using MoMo.Modules.LeadImporter.Domain.Model;

namespace MoMo.Modules.LeadImporter.Infrastructure.JsonSchemaDotNet;

public class JsonSchemaDotNetOpenApiService : IOpenApiService
{
    public OpenApiSchema ConvertSchema(Schema schema)
    {
        var jsonSchema = schema.JsonSchema.RootElement.GetRawText();
        var diagnostic = new OpenApiDiagnostic();
        var openApiSchema = new OpenApiStringReader().ReadFragment<OpenApiSchema>(
            jsonSchema, 
            OpenApiSpecVersion.OpenApi3_0, 
            out diagnostic);

        return openApiSchema;
    }
}