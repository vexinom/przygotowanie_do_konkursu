using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public class ResetPasswordDto
{
    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Token { get; set; }

    [Required]
    [MinLength(8)]
    public required string NewPassword { get; set; }
}