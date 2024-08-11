using MediatR;
using MoMo.Modules.LeadImporter.Domain.Model;
using MoMo.Modules.LeadImporter.Domain.Repositories;
using Newtonsoft.Json.Schema;

namespace MoMo.Modules.LeadImporter.Application.Commands.Schemas.Create;

public class CreateSchemaCommandHandler(ISchemaRepository schemaRepository) : IRequestHandler<CreateSchemaCommand, Guid>
{
    public async Task<Guid> Handle(CreateSchemaCommand request, CancellationToken cancellationToken)
    {
        var jsonSchema = JSchema.Parse(request.JsonSchema);
        var schema = new Schema(Guid.NewGuid(), jsonSchema);
        await schemaRepository.AddAsync(schema, cancellationToken);

        return schema.Id;
    }
}