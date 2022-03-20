using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class PhoneNumber
{
    [Column("PhoneNumberId")]
    public int Id { get; set; }
    
    public PhoneNumberType PhoneNumberType { get; set; }
    
    [Required(ErrorMessage = "PhoneNumber is a required field.")]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "The length for the phone number is from 4 to 50 characters.")]
    public string? Number { get; set; }
}