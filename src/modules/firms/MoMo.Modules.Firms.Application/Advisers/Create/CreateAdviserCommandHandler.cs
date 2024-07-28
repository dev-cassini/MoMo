using MediatR;
using MoMo.BuildingBlocks.Application.Exceptions;
using MoMo.Modules.Firms.Domain.Model;
using MoMo.Modules.Firms.Domain.Repositories;

namespace MoMo.Modules.Firms.Application.Advisers.Create;

internal class CreateAdviserCommandHandler(
    IArFirmRepository arFirmRepository) : IRequestHandler<CreateAdviserCommand, Guid>
{
    public async Task<Guid> Handle(CreateAdviserCommand request, CancellationToken cancellationToken)
    {
        var arFirm = await arFirmRepository.GetAsync(request.ArFirmId, cancellationToken);
        if (arFirm is null)
        {
            throw new BadRequestExceptions.NotFoundException(request.ArFirmId, typeof(ArFirm));
        }

        var adviser = request.ToAdviser(arFirm);
        arFirm.AddAdviser(adviser);

        return adviser.Id;
    }
}