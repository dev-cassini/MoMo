using System.Text.Json;
using System.Text.Json.Nodes;
using Json.More;
using Json.Schema;
using MediatR;
using MoMo.Modules.LeadImporter.Application.Services;

namespace MoMo.Modules.LeadImporter.Application.Commands;

internal class ImportLeadCommandHandler(
    IImportLeadService importLeadService) : IRequestHandler<ImportLeadCommandV2, ImportLeadResponse>
{
    public async Task<ImportLeadResponse> Handle(ImportLeadCommandV2 command, CancellationToken cancellationToken)
    {
        var jsonSchema = JsonSchema.FromText("""
                                             {
                                                 "$schema": "https://json-schema.org/draft/2019-09/schema",
                                                 "type": "object",
                                                 "title": "Root Schema",
                                                 "required": [
                                                     "adviserId",
                                                     "test",
                                                     "leadCustomers"
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
                                                     },
                                                     "leadCustomers": {
                                                         "type": "array",
                                                         "default": [],
                                                         "title": "The leadCustomers Schema",
                                                         "items": {
                                                             "type": "object",
                                                             "title": "A Schema",
                                                             "required": [
                                                                 "firstName",
                                                                 "lastName",
                                                                 "emailAddress"
                                                             ],
                                                             "additionalProperties": false,
                                                             "properties": {
                                                                 "firstName": {
                                                                     "type": "string",
                                                                     "title": "The firstName Schema"
                                                                 },
                                                                 "lastName": {
                                                                     "type": "string",
                                                                     "title": "The lastName Schema"
                                                                 },
                                                                 "emailAddress": {
                                                                     "type": "string",
                                                                     "title": "The emailAddress Schema"
                                                                 }
                                                             }
                                                         }
                                                     }
                                                 }
                                             }
                                             """);

        var jsonObject = JsonObject.Create(command.Request);
        var evaluationResult = jsonSchema.Evaluate(jsonObject);
        
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        var serviceRequest = command.Request.Deserialize<IImportLeadService.ImportLeadRequest>();
        var leadId = await importLeadService.ExecuteAsync(serviceRequest!, cancellationToken);

        return new ImportLeadResponse(leadId);
    }
}