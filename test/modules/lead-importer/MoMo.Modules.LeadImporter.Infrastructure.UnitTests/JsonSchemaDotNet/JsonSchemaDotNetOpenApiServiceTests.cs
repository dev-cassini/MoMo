using System.Text.Json;
using Json.Schema;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;
using MoMo.Modules.LeadImporter.Domain.Model;
using MoMo.Modules.LeadImporter.Infrastructure.JsonSchemaDotNet;

namespace MoMo.Modules.LeadImporter.Infrastructure.UnitTests.JsonSchemaDotNet;

[TestFixture]
public class JsonSchemaDotNetOpenApiServiceTests
{
    private record Track(int Number, string Title, int LengthInSeconds);
    private record Album(string Title, IEnumerable<Track> Tracks);

    private JsonDocument _albumJsonSchema;

    [SetUp]
    public void SetUp()
    {
        var trackNumberProperty = ("number", new JsonSchemaBuilder().Type(SchemaValueType.Integer).Build());
        var trackTitleProperty = ("title", new JsonSchemaBuilder().Type(SchemaValueType.String).Build());
        var trackLengthProperty = ("lengthInSeconds", new JsonSchemaBuilder().Type(SchemaValueType.Integer).Build());

        var trackJsonSchema = new JsonSchemaBuilder()
            .Title("Track")
            .Properties([trackNumberProperty, trackTitleProperty, trackLengthProperty])
            .Required(["number", "title", "lengthInSeconds"])
            .Build();
        
        var titleProperty = ("title", new JsonSchemaBuilder().Type(SchemaValueType.String).Build());
        var tracksProperty = ("tracks", new JsonSchemaBuilder().Type(SchemaValueType.Array).Items(trackJsonSchema).Build());
        var albumJsonSchema = new JsonSchemaBuilder()
            .Title("Album")
            .Properties([titleProperty, tracksProperty])
            .Required(["title", "tracks"])
            .Build();
        
        _albumJsonSchema = JsonSerializer.SerializeToDocument(albumJsonSchema);
    }

    [TearDown]
    public void TearDown()
    {
        _albumJsonSchema.Dispose();
    }
    
    [Test]
    public void ConvertSchema()
    {
        var sut = new JsonSchemaDotNetOpenApiService();
        var openApiSchema = sut.ConvertSchema(new Schema(Guid.NewGuid(), DateTimeOffset.UtcNow, _albumJsonSchema));
        
        var openApiDocument = new OpenApiDocument
        {
            Info = new OpenApiInfo
            {
                Title = "Create Album",
                Description = "Create a new album with tracks."
            },
            Components = new OpenApiComponents
            {
                Schemas = new Dictionary<string, OpenApiSchema>
                {
                    { "request", openApiSchema }
                }
            },
        };
        
        using var streamWriter = new StreamWriter("../../../JsonSchemaDotNet/test-open-api-spec.json");
        var writer = new OpenApiJsonWriter(streamWriter);
        openApiDocument.SerializeAsV3(writer);
    }
}