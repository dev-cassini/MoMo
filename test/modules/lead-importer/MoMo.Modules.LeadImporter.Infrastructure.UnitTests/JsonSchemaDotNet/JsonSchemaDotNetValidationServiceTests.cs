using System.Text.Json;
using Json.Schema;
using MoMo.Modules.LeadImporter.Domain.Model;
using MoMo.Modules.LeadImporter.Infrastructure.JsonSchemaDotNet;

namespace MoMo.Modules.LeadImporter.Infrastructure.UnitTests.JsonSchemaDotNet;

[TestFixture]
public class JsonSchemaDotNetValidationServiceTests
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
            .Schema("https://json-schema.org/draft/2020-12/schema")
            .Title("Track")
            .Properties([trackNumberProperty, trackTitleProperty, trackLengthProperty])
            .Required(["number", "title", "lengthInSeconds"])
            .Build();
        
        var titleProperty = ("title", new JsonSchemaBuilder().Type(SchemaValueType.String).Build());
        var tracksProperty = ("tracks", new JsonSchemaBuilder().Type(SchemaValueType.Array).Items(trackJsonSchema).Build());
        var albumJsonSchema = new JsonSchemaBuilder()
            .Schema("https://json-schema.org/draft/2020-12/schema")
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
    public void ValidatorHandlesPascalCaseRequest()
    {
        // Arrange
        var schema = new Schema(Guid.NewGuid(), _albumJsonSchema);
        var sut = new JsonSchemaDotNetValidationService();
        var testRequest = new Album(
            "Speakerboxxx",
            [
                new Track(1, "Intro", 89),
                new Track(2, "GhettoMusick", 236)
            ]);

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var jsonNode = JsonSerializer.SerializeToNode(testRequest, jsonSerializerOptions)!;
        
        // Act
        var (isValid, _) = sut.Validate(jsonNode, schema);
        
        // Act
        Assert.That(isValid, Is.True);
    }
    
    [Test]
    public void ValidatorHandlesCamelCaseRequest()
    {
        // Arrange
        var schema = new Schema(Guid.NewGuid(), _albumJsonSchema);
        var sut = new JsonSchemaDotNetValidationService();
        var testRequest = new Album(
            "Speakerboxxx",
            [
                new Track(1, "Intro", 89),
                new Track(2, "GhettoMusick", 236)
            ]);

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var jsonNode = JsonSerializer.SerializeToNode(testRequest, jsonSerializerOptions)!;
        
        // Act
        var (isValid, _) = sut.Validate(jsonNode, schema);
        
        // Act
        Assert.That(isValid, Is.True);
    }
    
    [Test]
    public void ValidatorHandlesSnakeCaseLowerRequest()
    {
        // Arrange
        var schema = new Schema(Guid.NewGuid(), _albumJsonSchema);
        var sut = new JsonSchemaDotNetValidationService();
        var testRequest = new Album(
            "Speakerboxxx",
            [
                new Track(1, "Intro", 89),
                new Track(2, "GhettoMusick", 236)
            ]);

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };

        var jsonNode = JsonSerializer.SerializeToNode(testRequest, jsonSerializerOptions)!;
        
        // Act
        var (isValid, _) = sut.Validate(jsonNode, schema);
        
        // Act
        Assert.That(isValid, Is.True);
    }
    
    [Test]
    public void ValidatorHandlesSnakeCaseUpperRequest()
    {
        // Arrange
        var schema = new Schema(Guid.NewGuid(), _albumJsonSchema);
        var sut = new JsonSchemaDotNetValidationService();
        var testRequest = new Album(
            "Speakerboxxx",
            [
                new Track(1, "Intro", 89),
                new Track(2, "GhettoMusick", 236)
            ]);

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseUpper
        };

        var jsonNode = JsonSerializer.SerializeToNode(testRequest, jsonSerializerOptions)!;
        
        // Act
        var (isValid, _) = sut.Validate(jsonNode, schema);
        
        // Act
        Assert.That(isValid, Is.True);
    }
    
    [Test]
    public void ValidatorHandlesKebabCaseLowerRequest()
    {
        // Arrange
        var schema = new Schema(Guid.NewGuid(), _albumJsonSchema);
        var sut = new JsonSchemaDotNetValidationService();
        var testRequest = new Album(
            "Speakerboxxx",
            [
                new Track(1, "Intro", 89),
                new Track(2, "GhettoMusick", 236)
            ]);

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower
        };

        var jsonNode = JsonSerializer.SerializeToNode(testRequest, jsonSerializerOptions)!;
        
        // Act
        var (isValid, _) = sut.Validate(jsonNode, schema);
        
        // Act
        Assert.That(isValid, Is.True);
    }
    
    [Test]
    public void ValidatorHandlesKebabCaseUpperRequest()
    {
        // Arrange
        var schema = new Schema(Guid.NewGuid(), _albumJsonSchema);
        var sut = new JsonSchemaDotNetValidationService();
        var testRequest = new Album(
            "Speakerboxxx",
            [
                new Track(1, "Intro", 89),
                new Track(2, "GhettoMusick", 236)
            ]);

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.KebabCaseUpper
        };

        var jsonNode = JsonSerializer.SerializeToNode(testRequest, jsonSerializerOptions)!;
        
        // Act
        var (isValid, _) = sut.Validate(jsonNode, schema);
        
        // Act
        Assert.That(isValid, Is.True);
    }
}