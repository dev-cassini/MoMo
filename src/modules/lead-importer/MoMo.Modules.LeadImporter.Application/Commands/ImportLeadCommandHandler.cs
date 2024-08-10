using MediatR;
using MoMo.Modules.LeadImporter.Application.Services;

namespace MoMo.Modules.LeadImporter.Application.Commands;

internal class ImportLeadCommandHandler(
    IImportLeadService importLeadService) : IRequestHandler<ImportLeadCommand, ImportLeadResponse>
{
    public async Task<ImportLeadResponse> Handle(ImportLeadCommand command, CancellationToken cancellationToken)
    {
        var serviceRequest = command.ToServiceRequest();
        var leadId = await importLeadService.ExecuteAsync(serviceRequest, cancellationToken);

        return new ImportLeadResponse(leadId);
    }
}