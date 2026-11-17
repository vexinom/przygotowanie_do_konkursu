using Backend.Models;

namespace Backend.DTOs;

public class UpdateHistoryDto
{
    public required SolveState State { get; set; }
    public required DateTime LastInteractionTime { get; set; }
}
