namespace Backend.DTOs;
public class UpdateAnswerDto 
{
    public required Guid? Id { get; set; } //null = nowa odpowiedź
    public required string Content { get; set; }
    public required bool IsCorrect { get; set; }
}