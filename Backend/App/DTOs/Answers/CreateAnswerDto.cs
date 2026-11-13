namespace Backend.DTOs;
public class CreateAnswerDto
{
    public required Guid ProblemId { get; set; }
    public required string Content { get; set; }
    public required bool IsCorrect { get; set; }
}