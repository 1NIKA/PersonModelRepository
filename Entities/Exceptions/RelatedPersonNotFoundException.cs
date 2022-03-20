namespace Entities.Exceptions;

public class RelatedPersonNotFoundException : NotFoundException
{
    public RelatedPersonNotFoundException(int personId)
        : base($"This Person hasn't related person with id: {personId}")
    {
    }
}