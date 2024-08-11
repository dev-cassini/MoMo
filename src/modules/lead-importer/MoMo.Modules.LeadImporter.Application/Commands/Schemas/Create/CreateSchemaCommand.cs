using MediatR;

namespace MoMo.Modules.LeadImporter.Application.Commands.Schemas.Create;

public record CreateSchemaCommand(string JsonSchema) : IRequest<Guid>;