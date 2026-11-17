using System.ComponentModel.DataAnnotations;
using Backend.Models;

namespace Backend.DTOs;

public class CreateHistoryDto
{
    [Required]
    public required SolveState State { get; set; }
    [Required]
    public required DateTime LastInteractionTime { get; set; }
    [Required]
    public required Guid ProblemId { get; set; }
}
