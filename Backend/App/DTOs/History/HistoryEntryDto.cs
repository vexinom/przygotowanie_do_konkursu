namespace Backend.DTOs;

public class HistoryEntryDto
{
    public required Guid ProblemId { get; set; }

    public required bool IsCorrect { get; set; }

    public DateTime FirstAttemptAt { get; set; }
    public DateTime LastAttemptAt { get; set; }

    public int AttemptsToSolve { get; set; }
    public int TotalAttempts { get; set; }

    public DateTime? FirstSolvedAt { get; set; }
}
