using MediatR;
using MoMo.Modules.LeadImporter.Domain.Model;
using MoMo.Modules.LeadImporter.Domain.Repositories;

namespace MoMo.Modules.LeadImporter.Application.Commands.Schemas.Create;

public class CreateSchemaCommandHandler(ISchemaRepository schemaRepository) : IRequestHandler<CreateSchemaCommand, Guid>
{
    public async Task<Guid> Handle(CreateSchemaCommand request, CancellationToken cancellationToken)
    {
        var schema = new Schema(Guid.NewGuid(), DateTimeOffset.UtcNow, request.JsonSchema);
        await schemaRepository.AddAsync(schema, cancellationToken);

        return schema.Id;
    }
}