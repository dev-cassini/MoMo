namespace MoMo.Modules.LeadDistribution.Domain.Model;

internal class Recipient(Guid id, string name, bool enabled)
{
    internal Guid Id { get; } = id;
    internal string Name { get; } = name;
    internal bool Enabled { get; } = enabled;
}