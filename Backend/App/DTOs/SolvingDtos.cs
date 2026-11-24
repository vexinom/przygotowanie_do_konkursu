using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public class AnswerOptionDto
{
    public Guid Id { get; set; }
    public string Content { get; set; } = null!;
}

public class ProblemToSolveDto
{
    public Guid Id { get; set; }
    public string Content { get; set; } = null!;
    public List<AnswerOptionDto> Answers { get; set; } = new();
}

public class SubmitAnswerDto
{
    public required Guid ProblemId { get; set; }

    [MinLength(1)]
    public required List<Guid> SelectedAnswerIds { get; set; }
}

public class AnswerResultDto
{
    public bool IsCorrect { get; set; }
    public List<Guid> CorrectAnswerIds { get; set; } = new();
}