using System.Text.Json;

namespace MoMo.Modules.LeadImporter.Domain.Model;

public class Schema(Guid id, DateTimeOffset creationTimestampUtc, JsonDocument jsonSchema)
{
    public Guid Id { get; } = id;
    public DateTimeOffset CreationTimestampUtc { get; } = creationTimestampUtc;
    public JsonDocument JsonSchema { get; } = jsonSchema;
}