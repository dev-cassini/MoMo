using MoMo.Modules.LeadDistribution.Domain.Model;

namespace MoMo.Modules.LeadDistribution.Builders;

internal class RecipientBuilder
{
    private bool _enabled = true;
    
    internal Recipient Build()
    {
        return new Recipient(
            Guid.NewGuid(),
            "Test Recipient",
            true);
    }

    internal RecipientBuilder WithEnabled(bool enabled)
    {
        _enabled = enabled;
        return this;
    }
}