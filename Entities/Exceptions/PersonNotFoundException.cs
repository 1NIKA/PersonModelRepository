namespace Entities.Exceptions;

public class PersonNotFoundException : NotFoundException
{
    public PersonNotFoundException(int personId) 
        : base($"The Person with id: {personId} doesn't exists in the database.")
    {
    }
}