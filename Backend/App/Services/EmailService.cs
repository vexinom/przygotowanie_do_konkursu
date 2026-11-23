using FluentEmail.Core;

namespace Backend.Services;

public class EmailService : IEmailService
{
    private readonly IFluentEmail _email;

    public EmailService(IFluentEmail email)
    {
        _email = email;
    }

    public async Task SendPasswordResetEmail(string email, string resetLink)
    {
        var response = await _email
            .To(email)
            .Subject("Reset hasła")
            .Body($"""
                <h2>Resetowanie hasła</h2>
                <p>Otrzymaliśmy prośbę o zresetowanie hasła do Twojego konta.</p>
                <p>Kliknij poniższy link, aby ustawić nowe hasło:</p>
                <a href="{resetLink}">Zresetuj hasło</a>
                <p>Jeśli nie prosiłeś o resetowanie hasła, zignoruj tę wiadomość.</p>
                """, isHtml: true)
            .SendAsync();

        if (!response.Successful)
        {
            throw new InvalidOperationException(
                $"Nie udało się wysłać emaila: {string.Join(", ", response.ErrorMessages)}"
            );
        }
    }

    public async Task SendConfirmationEmail(string email, string confirmEmailLink)
    {
        var response = await _email
            .To(email)
            .Subject("Reset hasła")
            .Body($"""
                <h2>Potwierdzenie adresu email</h2>
                <p>Cieszymy się, że dołączasz do nas</p>
                <p>Kliknij poniższy link, aby potwierdzić swój email:</p>
                <a href="{confirmEmailLink}">Potwierdź</a>
                <p>Jeśli nie zakładałeś konta, zignoruj tę wiadomość.</p>
                """, isHtml: true)
            .SendAsync();

        if (!response.Successful)
        {
            throw new InvalidOperationException(
                $"Nie udało się wysłać emaila: {string.Join(", ", response.ErrorMessages)}"
            );
        }
    }
}