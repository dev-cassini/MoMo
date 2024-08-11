using MediatR;
using Newtonsoft.Json.Linq;

namespace MoMo.Modules.LeadImporter.Application.Commands;

public record ImportLeadCommand(JObject Request) : IRequest<ImportLeadResponse>;

public record ImportLeadResponse(Guid LeadId);