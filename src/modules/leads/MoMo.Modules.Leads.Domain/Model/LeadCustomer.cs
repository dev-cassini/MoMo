namespace MoMo.Domain.Model;

internal class LeadCustomer(
    Guid id, 
    string firstName, 
    string lastName, 
    string emailAddress,
    Lead lead)
{
    internal Guid Id { get; } = id;
    internal string FirstName { get; } = firstName;
    internal string LastName { get; } = lastName;
    internal string EmailAddress { get; } = emailAddress;
    internal Guid LeadId { get; } = lead.Id;
}