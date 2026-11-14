using Backend.Models;

namespace Backend.DTOs;
public class ProblemDto
{
    public required Guid Id { get; set; }
    public required string Content { get; set; }
    public required List<AnswerDto> Answers { get; set; }

    public Guid? ContestId { get; set; } 
}
