using System.Text.Json.Nodes;
using MoMo.Modules.LeadImporter.Domain.Model;

namespace MoMo.Modules.LeadImporter.Application.Services;

public interface IJsonValidationService
{
    (bool IsValid, IDictionary<string, string> ValidationErrors) Validate(JsonNode jsonNode, Schema schema);
}