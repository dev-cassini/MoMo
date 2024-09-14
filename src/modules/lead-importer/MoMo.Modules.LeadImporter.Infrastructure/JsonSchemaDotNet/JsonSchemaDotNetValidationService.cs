using System.Text.Json;
using System.Text.Json.Nodes;
using Humanizer;
using Json.Schema;
using MoMo.Modules.LeadImporter.Application.Services;
using MoMo.Modules.LeadImporter.Domain.Model;

namespace MoMo.Modules.LeadImporter.Infrastructure.JsonSchemaDotNet;

public class JsonSchemaDotNetValidationService : IJsonValidationService
{
    public (bool IsValid, IDictionary<string, string> ValidationErrors) Validate(JsonNode jsonNode, Schema schema)
    {
        if (jsonNode is not JsonObject jsonObject)
        {
            return (false, new Dictionary<string, string>());
        }
        
        var jsonSchema = schema.JsonSchema.Deserialize<JsonSchema>()!;
        var evaluationOptions = new EvaluationOptions { OutputFormat = OutputFormat.List };
        
        CamelCasePropertyNames(jsonObject);
        var evaluationResults = jsonSchema.Evaluate(jsonObject, evaluationOptions);

        return (evaluationResults.IsValid, new Dictionary<string, string>());
    }
    
    private static void CamelCasePropertyNames(JsonObject jsonObject)
    {
        var propertyNames = jsonObject.Select(x => x.Key).ToList();
        foreach (var propertyName in propertyNames)
        {
            var adjustedName = propertyName.Camelize();  
            var value = jsonObject[propertyName];
            jsonObject.Remove(propertyName);
            jsonObject[adjustedName] = value;

            if (value is JsonArray jsonArray)
            {
                foreach (var jsonArrayItem in jsonArray)
                {
                    if (jsonArrayItem is JsonObject jsonArrayItemObject)
                    {
                        CamelCasePropertyNames(jsonArrayItemObject);
                    }
                }

                continue;
            }

            if (value is JsonObject valueJsonObject)
            {
                CamelCasePropertyNames(valueJsonObject);
            }
        }
    }
}