using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Persons.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Persons.Presentation.Controllers;

[Route("api/persons")]
[ApiController]
public class PersonsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public PersonsController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllPersons([FromQuery]PersonParameters personParameters)
    {
        var pagedResult = 
            await _serviceManager.PersonService.GetAllPersons(personParameters, false);
        
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
        
        return Ok(pagedResult.persons);
    }
    
    [HttpGet("{id:int}", Name = "PersonById")]
    public async Task<IActionResult> GetPerson(int id)
    {
        var person = await _serviceManager.PersonService.GetPerson(id, false);
        return Ok(person);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreatePerson([FromBody]PersonForCreationDto person)
    {
        var createdPerson = await _serviceManager.PersonService.CreatePerson(person);
        return CreatedAtRoute("PersonById", new {id = createdPerson.Id}, createdPerson);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        await _serviceManager.PersonService.DeletePerson(id, false);
        return NoContent();
    }
    
    [HttpPut("{id:int}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdatePerson(int id, [FromBody]PersonForUpdateDto person)
    {
        await _serviceManager.PersonService.UpdatePerson(id, person, true);
        return NoContent();
    }
    
    [HttpPost("{id:int}/related-person")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> AddRelatedPerson(
        int id, [FromBody]RelationPersonDto relationPerson)
    {
        var updatedPerson = await _serviceManager.PersonService.AddRelationPerson(
            id, relationPerson, true);
        return CreatedAtRoute("PersonById", new {id = updatedPerson.Id}, updatedPerson);
    }
    
    [HttpDelete("{id:int}/related-person/{relatedPersonId:int}")]
    public async Task<IActionResult> DeleteRelatedPerson(int id, int relatedPersonId)
    {
        await _serviceManager.PersonService.DeleteRelatedPerson(id, relatedPersonId, true);
        return NoContent();
    }
    
    [HttpGet("{id:int}/related-person/{relationType}")]
    public async Task<IActionResult> GetRelatedPersonCount(int id, RelationPersonTypeDto relationType)
    {
        var relatedPersonCount = 
            await _serviceManager.PersonService.GetRelatedPersonCount(id, relationType, true);
        
        return Ok($"This person has {relatedPersonCount} related person");
    }
}