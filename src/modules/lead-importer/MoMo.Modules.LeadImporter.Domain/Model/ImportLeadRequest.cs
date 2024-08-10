namespace MoMo.Modules.LeadImporter.Domain.Model;

internal class ImportLeadRequest(
    Guid id,
    string actor,
    DateTimeOffset timestampUtc,
    string request,
    int responseStatusCode,
    Guid? leadId)
{
    internal Guid Id { get; } = id;
    internal string Actor { get; } = actor;
    internal DateTimeOffset TimestampUtc { get; } = timestampUtc;
    internal string Request { get; } = request;
    internal int ResponseStatusCode { get; } = responseStatusCode;
    internal Guid? LeadId { get; } = leadId;
}