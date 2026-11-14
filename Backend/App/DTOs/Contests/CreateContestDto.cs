namespace Backend.DTOs;

public class CreateContestDto
{
    public required string Name { get; set; } 
    public string? Description { get; set; }
}