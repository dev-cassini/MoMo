namespace MoMo.Modules.LeadDistribution.Domain.Model;

internal class RecipientConfiguration(
    Guid id,
    bool enabled,
    int weighting,
    Recipient recipient,
    DistributionGroup distributionGroup)
{
    internal Guid Id { get; } = id;
    internal bool Enabled { get; } = enabled;
    internal int Weighting { get; } = weighting;
    internal Guid RecipientId { get; } = recipient.Id;
    internal Guid DistributionGroupId { get; } = distributionGroup.Id;
}