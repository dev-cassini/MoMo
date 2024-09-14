using System.Text.Json;
using MediatR;

namespace MoMo.Modules.LeadImporter.Application.Commands.Schemas.Create;

public record CreateSchemaCommand(JsonDocument JsonSchema) : IRequest<Guid>;