using System.Text.Json;

namespace MoMo.Modules.LeadImporter.Domain.Model;

public class Schema(Guid id, JsonDocument jsonSchema)
{
    public Guid Id { get; } = id;
    public JsonDocument JsonSchema { get; } = jsonSchema;
}