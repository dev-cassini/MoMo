namespace MoMo.Modules.Firms.Domain.Model;

internal class Adviser(
    Guid id, 
    string firstName, 
    string lastName, 
    string emailAddress,
    ArFirm arFirm)
{
    internal Guid Id { get; } = id;
    internal string FirstName { get; } = firstName;
    internal string LastName { get; } = lastName;
    internal string EmailAddress { get; } = emailAddress;
    internal Guid ArFirmId { get; } = arFirm.Id;
}