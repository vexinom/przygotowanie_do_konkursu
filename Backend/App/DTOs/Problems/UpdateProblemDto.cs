// using System.ComponentModel.DataAnnotations;
namespace Backend.DTOs;
public class UpdateProblemDto
{
    public required string Content { get; set; }
    
    // te warunki jeszcze do dodania!
    // [MinLength(2, ErrorMessage = "At least two answers must be added.")]
    public required List<UpdateAnswerDto> Answers { get; set; }
    public required Guid ContestId { get; set; } 
}
