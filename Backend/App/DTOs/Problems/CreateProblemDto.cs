using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;
public class CreateProblemDto
{
    [Required]
    public required string Content { get; set; }
    
    [Required]
    [MinLength(2, ErrorMessage = "At least two answers must be added.")]
    public required List<CreateAnswerDto> Answers { get; set; }
    public Guid? ContestId { get; set; } 
}
