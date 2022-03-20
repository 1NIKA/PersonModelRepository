using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IPersonRepository
{
    Task<PagedList<Person?>> GetAllPersons(PersonParameters personParameters, bool trackChanges);
    Task<Person?> GetPerson(int personId, bool trackChanges);
    Task CreatePerson(Person person);
    Task DeletePerson(Person person);
}