using MediatR;
using MoMo.Modules.Firms.Domain.Model;

namespace MoMo.Modules.Firms.Application.Advisers.Create;

internal record CreateAdviserCommand(
    string FirstName,
    string LastName,
    string EmailAddress,
    Guid ArFirmId) : IRequest<Guid>;

internal static class CreateAdviserCommandExtensions
{
    internal static Adviser ToAdviser(this CreateAdviserCommand command, ArFirm arFirm)
    {
        return new Adviser(
            Guid.NewGuid(),
            command.FirstName,
            command.LastName,
            command.EmailAddress,
            arFirm);
    }
}