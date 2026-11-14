using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public class CreateContestDto
{
    [Required]
    public required string Name { get; set; } 
    public string? Description { get; set; }
}