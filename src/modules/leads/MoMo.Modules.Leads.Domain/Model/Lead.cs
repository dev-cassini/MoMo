namespace MoMo.Modules.Leads.Domain.Model;

internal class Lead(Guid id, Adviser adviser)
{
    internal Guid Id { get; } = id;
    internal Guid AdviserId { get; } = adviser.Id;
    internal Adviser Adviser { get; } = adviser;
    
    private readonly List<LeadCustomer> _customers = [];
    internal IReadOnlyList<LeadCustomer> Customers => _customers;

    internal void AddCustomer(LeadCustomer leadCustomer) => _customers.Add(leadCustomer);
}