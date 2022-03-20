namespace Shared.DataTransferObjects;

public class PersonDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public GenderTypeDto Gender { get; set; }
    public string? PersonalNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public string? City { get; set; }
    public IEnumerable<PhoneNumberDto>? PhoneNumbers { get; set; }
    public List<PersonDto> RelatedPersons { get; set; } = new List<PersonDto>();
    public byte[]? ImageAsBytes { get; set; }
}