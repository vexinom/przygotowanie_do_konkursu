namespace Backend.Models;

public class Answer
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsDeleted { get; set; } = false;
    public bool IsCorrect { get; set; } = false;
    public string Content { get; set;} = string.Empty;
    public Guid ProblemId { get; set; } = Guid.Empty;
}