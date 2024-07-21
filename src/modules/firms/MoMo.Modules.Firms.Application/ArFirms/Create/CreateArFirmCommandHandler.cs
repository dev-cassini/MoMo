using MediatR;
using MoMo.Modules.Firms.Domain.Model;
using MoMo.Modules.Firms.Domain.Repositories;

namespace MoMo.Modules.Firms.Application.ArFirms.Create;

internal class CreateArFirmCommandHandler(
    IArFirmRepository arFirmRepository) : IRequestHandler<CreateArFirmCommand, Guid>
{
    public async Task<Guid> Handle(CreateArFirmCommand request, CancellationToken cancellationToken)
    {
        var arFirm = new ArFirm(Guid.NewGuid(), request.Name);
        await arFirmRepository.AddAsync(arFirm, cancellationToken);

        return arFirm.Id;
    }
}