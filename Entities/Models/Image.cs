using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Image
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The image path i a required field.")]
    [StringLength(250, ErrorMessage = "The length for the image name is 250 characters.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "The image path i a required field.")]
    public string? Path { get; set; }
}