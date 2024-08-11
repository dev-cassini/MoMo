using System.Text.Json.Nodes;
using Json.Schema;
using MoMo.Modules.LeadImporter.Application.Commands.Schemas.Create;

namespace MoMo.Modules.LeadImporter.Application.UnitTests.Commands.Schemas;

[TestFixture]
public class CreateSchemaCommandHandlerTests
{
    [Test]
    public async Task Test1()
    {
        var schema = new JsonSchemaBuilder()
            .Comment("a comment")
            .Title("A title for my schema")
            .Type(SchemaValueType.Object)
            .Properties(
                ("foo", new JsonSchemaBuilder()
                    .Type(SchemaValueType.String)
                ),
                ("bar", new JsonSchemaBuilder()
                    .Type(SchemaValueType.Number)
                )
            )
            .Build();
        
        var instance = JsonNode.Parse("{\"foo\":\"a value\",\"bar\":\"a value\"}");
        var results = schema.Evaluate(instance);
        foreach (var error in results.Errors ?? new Dictionary<string, string>())
        {
            Console.WriteLine(error.Key);
        }



        // var sut = new CreateSchemaCommandHandler();

        // Act
        // await sut.Handle(command, CancellationToken.None);
    }
}