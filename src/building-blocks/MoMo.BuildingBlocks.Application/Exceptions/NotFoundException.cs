namespace MoMo.BuildingBlocks.Application.Exceptions;

public class NotFoundException(Guid id, Type type)
    : Exception(string.Format("{0} with id {1} could not be found.", type.Name, id));