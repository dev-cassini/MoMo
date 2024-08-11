using MoMo.Modules.LeadImporter.Application.Commands.Schemas.Create;
using MoMo.Modules.LeadImporter.Domain.Repositories;
using Moq;

namespace MoMo.Modules.LeadImporter.Application.UnitTests.Commands.Schemas;

[TestFixture]
public class CreateSchemaCommandHandlerTests
{
    [Test]
    public async Task Test1()
    {
        // Arrange
        const string schema = """
                      {
                          "$schema": "https://json-schema.org/draft/2019-09/schema",
                          "type": "object",
                          "title": "Root Schema",
                          "required": [
                              "adviserId",
                              "test",
                          ],
                          "additionalProperties": false,
                          "properties": {
                              "adviserId": {
                                  "type": "string",
                                  "title": "The adviserId Schema"
                              },
                              "test": {
                                  "type": "string",
                                  "title": "The test Schema"
                              }
                          }
                      }
                      """;
        var command = new CreateSchemaCommand(schema);

        var schemaRepository = new Mock<ISchemaRepository>();
        var sut = new CreateSchemaCommandHandler(schemaRepository.Object);

        // Act
        await sut.Handle(command, CancellationToken.None);
    }
}