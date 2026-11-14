// using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;
public class UpdateProblemDto
{
    [Required]
    public required string Content { get; set; }
   
    [Required]
    [MinLength(2, ErrorMessage = "At least two answers must be added.")]
    public required List<UpdateAnswerDto> Answers { get; set; }
    public Guid ContestId { get; set; } 
}
