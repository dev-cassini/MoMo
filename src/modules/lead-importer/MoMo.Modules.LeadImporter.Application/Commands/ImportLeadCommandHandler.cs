using MediatR;
using MoMo.Modules.LeadImporter.Application.Services;
using MoMo.Modules.LeadImporter.Domain.Repositories;
using Newtonsoft.Json.Schema;

namespace MoMo.Modules.LeadImporter.Application.Commands;

internal class ImportLeadCommandHandler(
    ISchemaRepository schemaRepository,
    IImportLeadService importLeadService) : IRequestHandler<ImportLeadCommand, ImportLeadResponse>
{
    public async Task<ImportLeadResponse> Handle(ImportLeadCommand command, CancellationToken cancellationToken)
    {
        var schema = await schemaRepository.GetSchemaAsync(cancellationToken);
        var requestIsValid = command.Request.IsValid(schema.JsonSchema, out IList<ValidationError> validationErrors);
        var serviceRequest = command.Request.ToObject<IImportLeadService.ImportLeadRequest>();
        var leadId = await importLeadService.ExecuteAsync(serviceRequest!, cancellationToken);

        return new ImportLeadResponse(leadId);
    }
}