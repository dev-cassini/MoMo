namespace MoMo.Modules.LeadDistribution.Domain.Model;

internal class DistributionGroup(
    Guid id,
    string name,
    bool enabled,
    Guid introducerFirmId)
{
    internal Guid Id { get; } = id;
    internal string Name { get; } = name;
    internal bool Enabled { get; } = enabled;
    internal Guid IntroducerFirmId { get; } = introducerFirmId;

    private readonly List<RecipientConfiguration> _recipientConfigurations = [];
    internal IReadOnlyList<RecipientConfiguration> RecipientConfigurations => _recipientConfigurations;

    internal void AddRecipientConfiguration(RecipientConfiguration recipientConfiguration)
        => _recipientConfigurations.Add(recipientConfiguration);
}