using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record PersonForCreationDto
{
    [Required(ErrorMessage = "Person firstname is a required field.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "The length for the firstname is from 2 to 50 characters.")]
    public string? FirstName { get; init; }
    
    [Required(ErrorMessage = "Person lastname is a required field.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "The length for the lastname is from 2 to 50 characters.")]
    public string? LastName { get; init; }

    public GenderTypeDto Gender { get; init; }
    
    [Required(ErrorMessage = "Person personal number is a required field.")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "The person number value cannot exceed 11 characters.")]
    public string? PersonalNumber { get; init; }
    
    [Required(ErrorMessage = "Person birth date is a required field.")]
    [MinAge(18)]
    public DateTime BirthDate { get; init; }
    
    public string? City { get; init; }

    public IEnumerable<PhoneNumberDto>? PhoneNumbers { get; init; }
    public IEnumerable<RelationPersonDto>? RelationPersons { get; init; }
}