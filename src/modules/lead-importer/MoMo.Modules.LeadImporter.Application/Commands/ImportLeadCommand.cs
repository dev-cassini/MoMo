using System.Text.Json;
using MediatR;
using MoMo.Modules.LeadImporter.Application.Services;

namespace MoMo.Modules.LeadImporter.Application.Commands;

public record ImportLeadCommandV2(JsonElement Request) : IRequest<ImportLeadResponse>;

public record ImportLeadCommand(
    Guid AdviserId,
    IEnumerable<LeadCustomerDto> LeadCustomers) : IRequest<ImportLeadResponse>;

public record LeadCustomerDto(
    string FirstName,
    string LastName,
    string EmailAddress,
    bool Smoker);

public record ImportLeadResponse(Guid LeadId);
    
internal static class ImportLeadCommandExtensions
{
    internal static IImportLeadService.ImportLeadRequest ToServiceRequest(this ImportLeadCommand command)
    {
        return new IImportLeadService.ImportLeadRequest(
            command.AdviserId,
            command.LeadCustomers.Select(x => new IImportLeadService.LeadCustomerDto(
                x.FirstName,
                x.LastName,
                x.EmailAddress)));
    }
}