using Json.Schema;

namespace MoMo.Modules.LeadImporter.Domain.Model;

public class Schema(Guid id, JsonSchema jsonSchema)
{
    public Guid Id { get; } = id;
    public JsonSchema JsonSchema { get; } = jsonSchema;
}