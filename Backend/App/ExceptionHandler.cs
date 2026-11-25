using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Diagnostics;

namespace Backend.Exceptions;

public class ApiExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetails;
    private readonly ILogger<ApiExceptionHandler> _logger;

    public ApiExceptionHandler(
        IProblemDetailsService problemDetails,
        ILogger<ApiExceptionHandler> logger)
    {
        _problemDetails = problemDetails;
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext ctx, Exception ex, CancellationToken ct)
    {
        var (status, detail) = ex switch
        {
            UnauthorizedAccessException => (401, ex.Message),
            ForbiddenAccessException    => (403, ex.Message),
            KeyNotFoundException        => (404, ex.Message),
            ValidationException         => (400, ex.Message),
            EmailSendException          => (502, "Nie udało się wysłać wiadomości e-mail. Spróbuj ponownie później."),
            _                           => (0, "")
        };

        if (status == 0) return false;

        if (status >= 500)
            _logger.LogError(ex, "Błąd zewnętrznej usługi: {Message}", ex.Message);

        ctx.Response.StatusCode = status;

        return await _problemDetails.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = ctx,
            Exception = ex,
            ProblemDetails = { Status = status, Detail = detail }
        });
    }
}


public class ForbiddenAccessException : Exception { }
public class EmailSendException : Exception
{
    public EmailSendException(string message) : base(message) { }
}