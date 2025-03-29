using Microsoft.OpenApi.Models;
using MoMo.Modules.LeadImporter.Domain.Model;

namespace MoMo.Modules.LeadImporter.Application.Services;

public interface IOpenApiService
{
    OpenApiSchema ConvertSchema(Schema schema);
}