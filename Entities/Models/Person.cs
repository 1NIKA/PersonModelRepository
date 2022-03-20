using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Attributes;

namespace Entities.Models;

public class Person
{
    [Column("PersonId")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Person firstname is a required field.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "The length for the firstname is from 2 to 50 characters.")]
    public string? FirstName { get; set; }
    
    [Required(ErrorMessage = "Person lastname is a required field.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "The length for the lastname is from 2 to 50 characters.")]
    public string? LastName { get; set; }

    public GenderType Gender { get; set; }
    
    [Required(ErrorMessage = "Person personal number is a required field.")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "The person number value cannot exceed 11 characters.")]
    public string? PersonalNumber { get; set; }
    
    [Required(ErrorMessage = "Person birth date is a required field.")]
    [MinAge(18)]
    public DateTime BirthDate { get; set; }
    
    public string? City { get; set; }
    public ICollection<PhoneNumber>? PhoneNumbers { get; set; }

    public Image? Image { get; set; }
    
    public ICollection<RelationPerson>? RelationPersons { get; set; }
}