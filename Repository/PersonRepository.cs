using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

namespace Repository;

public class PersonRepository : RepositoryBase<Person>, IPersonRepository
{
    public PersonRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

    public async Task<PagedList<Person?>> GetAllPersons(PersonParameters personParameters, bool trackChanges)
    {
        var persons = await FindByCondition(p =>
                    personParameters.FirstName == null || p.FirstName!.Contains(personParameters.FirstName)
                    && personParameters.LastName == null || p.LastName!.Contains(personParameters.LastName!)
                    && personParameters.PersonalNumber == null ||
                    p.PersonalNumber!.Contains(personParameters.PersonalNumber!),
                trackChanges)
            .Include("RelationPersons")
            .Include("PhoneNumbers")
            .Include("Image")
            .OrderBy(p => p.FirstName)
            .ToListAsync();

        return PagedList<Person?>
            .ToPagedList(persons, personParameters.PageNumber, personParameters.PageSize);
    }

    public async Task<Person?> GetPerson(int personId, bool trackChanges) =>
        await FindByCondition(p => p.Id == personId, trackChanges)
            .Include("RelationPersons")
            .Include("PhoneNumbers")
            .Include("Image")
            .SingleOrDefaultAsync();

    public async Task CreatePerson(Person person) => await Create(person);
    public async Task DeletePerson(Person person) => await Delete(person);
}