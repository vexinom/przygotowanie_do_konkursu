using Backend.Models;

namespace Backend.DTOs;

public class HistoryDto
{
    public required Guid Id { get; set; }

    public required SolveState State { get; set; }
    public required DateTime LastInteractionTime { get; set; }

    public required Guid ProblemId { get; set; }
}
