// using System.ComponentModel.DataAnnotations;
namespace Backend.DTOs;
public class CreateProblemDto
{
    public required string Content { get; set; }
    
    // te warunki jeszcze do dodania!
    // [MinLength(2, ErrorMessage = "At least two answers must be added.")]
    public required List<CreateAnswerDto> Answers { get; set; }
    public Guid? ContestId { get; set; } 
}
