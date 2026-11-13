namespace Backend.DTOs;
public class ProblemDto
{
    public Guid Id { get; set; }
    public required string Content { get; set; }
    public List<AnswerDto> Answers { get; set; } = new();

    public List<Guid> CategoryIds { get; set; } = new();
}
