using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IPersonService
{
    Task<(IEnumerable<PersonDto?> persons, MetaData metaData)> GetAllPersons(
        PersonParameters personParameters, bool trackChanges);
    Task<PersonDto?> GetPerson(int personId, bool trackChanges);
    Task<PersonDto> CreatePerson(PersonForCreationDto person);
    Task DeletePerson(int personId, bool trackChanges);
    Task UpdatePerson(int personId, PersonForUpdateDto person, bool trackChanges);
    Task<PersonDto> AddRelationPerson(int personId, RelationPersonDto relationPerson, bool trackChanges);
    Task DeleteRelatedPerson(int personId, int relatedPersonId, bool trackChanges);
    Task<int> GetRelatedPersonCount(int id, RelationPersonTypeDto relationType, bool trackChanges);
}