namespace Backend.DTOs;
public class AnswerDto
{
    public Guid Id { get; set; }
    public required string Content { get; set; }
    public required bool IsCorrect { get; set; }
}
