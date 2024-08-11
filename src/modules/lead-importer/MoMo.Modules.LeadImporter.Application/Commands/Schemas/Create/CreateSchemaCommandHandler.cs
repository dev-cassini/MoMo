using Json.Schema;
using MediatR;

namespace MoMo.Modules.LeadImporter.Application.Commands.Schemas.Create;

public class CreateSchemaCommandHandler : IRequestHandler<CreateSchemaCommand, Guid>
{
    public async Task<Guid> Handle(CreateSchemaCommand request, CancellationToken cancellationToken)
    {
        var jsonSchema = JsonSchema.FromText(request.JsonSchema);
        return Guid.NewGuid();
    }
}