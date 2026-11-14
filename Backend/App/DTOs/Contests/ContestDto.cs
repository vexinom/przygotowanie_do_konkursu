namespace Backend.DTOs;

public class ContestDto
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }
    public string? Description { get; set; }

}