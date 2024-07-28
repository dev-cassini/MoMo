using MediatR;
using MoMo.Modules.Leads.Domain.Model;

namespace MoMo.Modules.Leads.Application.Commands.Create;

internal record CreateLeadCommand(
    Guid ArFirmId, 
    Guid AdviserId,
    IEnumerable<LeadCustomerDto> LeadCustomers) : IRequest<Guid>;

internal record LeadCustomerDto(
    string FirstName,
    string LastName,
    string EmailAddress);

internal static class CreateLeadCommandExtensions
{
    internal static Lead ToLead(this CreateLeadCommand command)
    {
        var lead = new Lead(Guid.NewGuid(), command.ArFirmId, command.AdviserId);
        foreach (var leadCustomerDto in command.LeadCustomers)
        {
            var leadCustomer = new LeadCustomer(
                Guid.NewGuid(),
                leadCustomerDto.FirstName,
                leadCustomerDto.LastName,
                leadCustomerDto.EmailAddress,
                lead);
            
            lead.AddCustomer(leadCustomer);
        }

        return lead;
    }
}