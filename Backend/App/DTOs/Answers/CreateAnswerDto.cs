using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;
public class CreateAnswerDto
{
    [Required]
    public required string Content { get; set; }
    
    public required bool IsCorrect { get; set; }
}