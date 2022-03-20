using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class RelationPerson
{
    [Column("RelationPersonId")]
    public int Id { get; set; }
    
    public RelationPersonType RelationPersonType { get; set; }

    [Required(ErrorMessage = "Related person id is a required field.")]
    public int RelatedPersonId { get; set; }
}