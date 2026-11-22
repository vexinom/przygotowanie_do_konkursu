using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;
public class ForgotPasswordDto
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
}
