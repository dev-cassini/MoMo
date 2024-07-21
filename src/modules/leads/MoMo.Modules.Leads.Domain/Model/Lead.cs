namespace MoMo.Modules.Leads.Domain.Model;

internal class Lead(Guid id, Guid? arFirmId, Guid? adviserId)
{
    internal Guid Id { get; } = id;
    internal Guid? ArFirmId { get; } = arFirmId;
    internal Guid? AdviserId { get; } = adviserId;
    
    private readonly List<LeadCustomer> _customers = [];
    internal IReadOnlyList<LeadCustomer> Customers => _customers;

    internal void AddCustomer(LeadCustomer customer) => _customers.Add(customer);
}