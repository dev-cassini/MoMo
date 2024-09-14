using System.Text.Json;
using MediatR;
using MoMo.Modules.LeadImporter.Application.Services;
using MoMo.Modules.LeadImporter.Domain.Repositories;

namespace MoMo.Modules.LeadImporter.Application.Commands;

internal class ImportLeadCommandHandler(
    ISchemaRepository schemaRepository,
    IJsonValidationService jsonValidationService,
    IImportLeadService importLeadService) : IRequestHandler<ImportLeadCommand, ImportLeadResponse>
{
    public async Task<ImportLeadResponse> Handle(ImportLeadCommand command, CancellationToken cancellationToken)
    {
        var schema = await schemaRepository.GetSchemaAsync(cancellationToken);
        var (isValid, _) = jsonValidationService.Validate(command.Request, schema);
        if (isValid is false)
        {
            throw new Exception();
        }

        var serviceRequest = command.Request.Deserialize<IImportLeadService.ImportLeadRequest>();
        var leadId = await importLeadService.ExecuteAsync(serviceRequest!, cancellationToken);

        return new ImportLeadResponse(leadId);
    }
}