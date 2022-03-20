using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

public class PersonService : IPersonService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public PersonService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<(IEnumerable<PersonDto?> persons, MetaData metaData)> GetAllPersons(
        PersonParameters personParameters, bool trackChanges)
    {
        var personsEntityWithMetaData = 
            await _repositoryManager.Person.GetAllPersons(personParameters, trackChanges);
        var personsDto = _mapper.Map<IEnumerable<PersonDto?>>(personsEntityWithMetaData);

        return (persons: personsDto, metaData: personsEntityWithMetaData.MetaData);
    }

    public async Task<PersonDto?> GetPerson(int personId, bool trackChanges)
    {
        var personEntity = await _repositoryManager.Person.GetPerson(personId, trackChanges);
        if (personEntity is null) throw new PersonNotFoundException(personId);

        var person = _mapper.Map<PersonDto>(personEntity);
        foreach (var relationPerson in personEntity.RelationPersons!)
        {
            var relatedPerson = await _repositoryManager.Person.GetPerson(relationPerson.RelatedPersonId, trackChanges);
            person.RelatedPersons.Add(_mapper.Map<PersonDto>(relatedPerson));
        }

        if (personEntity.Image != null) person.ImageAsBytes = await File.ReadAllBytesAsync(personEntity.Image.Path!);
        return person;
    }

    public async Task<PersonDto> CreatePerson(PersonForCreationDto person)
    {
        var personEntity = _mapper.Map<Person>(person);

        await _repositoryManager.Person.CreatePerson(personEntity);
        await _repositoryManager.Save();

        return _mapper.Map<PersonDto>(personEntity);
    }

    public async Task DeletePerson(int personId, bool trackChanges)
    {
        var person = await _repositoryManager.Person.GetPerson(personId, trackChanges);
        if (person is null) throw new PersonNotFoundException(personId);

        await _repositoryManager.Person.DeletePerson(person);
        await _repositoryManager.Save();
    }

    public async Task UpdatePerson(int personId, PersonForUpdateDto person, bool trackChanges)
    {
        var personEntity = await _repositoryManager.Person.GetPerson(personId, trackChanges);
        if (personEntity is null) throw new PersonNotFoundException(personId);

        _mapper.Map(person, personEntity);
        await _repositoryManager.Save();
    }

    public async Task<PersonDto> AddRelationPerson(
        int personId, RelationPersonDto relationPerson, bool trackChanges)
    {
        var personEntity = await _repositoryManager.Person.GetPerson(personId, trackChanges);
        if (personEntity is null) throw new PersonNotFoundException(personId);
        
        var relationPersonEntity = 
            await _repositoryManager.Person.GetPerson(relationPerson.RelatedPersonId, trackChanges);
        if (relationPersonEntity is null) throw new PersonNotFoundException(relationPerson.RelatedPersonId);

        personEntity.RelationPersons ??= new List<RelationPerson>();
        var relatedPerson = _mapper.Map<RelationPerson>(relationPerson);
        personEntity.RelationPersons.Add(relatedPerson);
        await _repositoryManager.Save();
        
        return _mapper.Map<PersonDto>(personEntity);
    }

    public async Task DeleteRelatedPerson(int personId, int relatedPersonId, bool trackChanges)
    {
        var personEntity = await _repositoryManager.Person.GetPerson(personId, trackChanges);
        if (personEntity is null) throw new PersonNotFoundException(personId);

        if (personEntity.RelationPersons == null) 
            throw new RelatedPersonNotFoundException(relatedPersonId);

        var relatedPerson = personEntity.RelationPersons
            .SingleOrDefault(r => r.RelatedPersonId == relatedPersonId);
        if (relatedPerson is null) throw new RelatedPersonNotFoundException(relatedPersonId);
        
        personEntity.RelationPersons.Remove(relatedPerson);
        await _repositoryManager.Save();
    }

    public async Task<int> GetRelatedPersonCount(int id, RelationPersonTypeDto relationType, bool trackChanges)
    {
        var person = await _repositoryManager.Person.GetPerson(id, trackChanges);
        if (person is null) throw new PersonNotFoundException(id);

        return person.RelationPersons?
            .Where(r => (int)r.RelationPersonType == (int)relationType).Count() ?? 0;
    }
}