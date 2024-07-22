namespace MoMo.BuildingBlocks.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(Guid id, Type type) 
        : base(string.Format("{0} with id {1} could not be found.", type.Name, id)) {}
}