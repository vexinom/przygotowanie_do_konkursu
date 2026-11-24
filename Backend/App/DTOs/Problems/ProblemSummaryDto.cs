namespace Backend.DTOs;

public class ProblemSummaryDto
{
    public Guid Id { get; set; }
    public string Content { get; set; } = null!;
    public int AnswersCount { get; set; }
    public Guid? ContestId { get; set; }
}