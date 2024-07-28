namespace MoMo.BuildingBlocks.Application.Exceptions;

public static class BadRequestExceptions
{
    public class BadRequestException(string message) : Exception(message);
    
    public class NotFoundException(Guid id, Type type)
        : BadRequestException(string.Format("{0} with id {1} could not be found.", type.Name, id));
}