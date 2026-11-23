namespace Backend.Services;

public interface IEmailService
{
    Task SendPasswordResetEmail(string email, string resetLink);
    Task SendConfirmationEmail(string email, string confirmEmailLink);
}