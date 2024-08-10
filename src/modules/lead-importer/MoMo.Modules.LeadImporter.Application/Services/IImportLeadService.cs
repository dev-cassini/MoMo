namespace MoMo.Modules.LeadImporter.Application.Services;

public interface IImportLeadService
{
    public record ImportLeadRequest(
        Guid AdviserId,
        IEnumerable<LeadCustomerDto> LeadCustomers);

    public record LeadCustomerDto(
        string FirstName,
        string LastName,
        string EmailAddress);

    Task<Guid> ExecuteAsync(ImportLeadRequest importLeadRequest, CancellationToken cancellationToken);
}