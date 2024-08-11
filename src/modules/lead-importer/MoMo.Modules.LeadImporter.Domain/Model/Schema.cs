using Newtonsoft.Json.Schema;

namespace MoMo.Modules.LeadImporter.Domain.Model;

public class Schema(Guid id, JSchema jsonSchema)
{
    public Guid Id { get; } = id;
    public JSchema JsonSchema { get; } = jsonSchema;
}