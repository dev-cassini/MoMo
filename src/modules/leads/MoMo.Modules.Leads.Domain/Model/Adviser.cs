namespace MoMo.Modules.Leads.Domain.Model;

public class Adviser(Guid id, Guid arFirmId)
{
    internal Guid Id { get; } = id;
    internal Guid ArFirmId { get; } = arFirmId;
}