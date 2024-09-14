using System.Text.Json.Nodes;
using MediatR;

namespace MoMo.Modules.LeadImporter.Application.Commands;

public record ImportLeadCommand(JsonNode Request) : IRequest<ImportLeadResponse>;

public record ImportLeadResponse(Guid LeadId);