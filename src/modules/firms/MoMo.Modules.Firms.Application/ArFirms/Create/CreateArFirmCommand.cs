using MediatR;

namespace MoMo.Modules.Firms.Application.ArFirms.Create;

internal record CreateArFirmCommand(string Name) : IRequest<Guid>;