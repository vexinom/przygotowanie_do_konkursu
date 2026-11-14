namespace Backend.DTOs;

public class ContestDto
{
    public Guid Id { get; set; }

    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;

}