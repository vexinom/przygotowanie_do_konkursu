using Microsoft.AspNetCore.Diagnostics;
using static Backend.Services.UserProgressService;

namespace Backend.Exceptions;

public class AccessExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext ctx, Exception ex, CancellationToken ct)
    {
        var status = ex switch
        {
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            ForbiddenAccessException    => StatusCodes.Status403Forbidden,
            KeyNotFoundException        => StatusCodes.Status404NotFound,
            _ => (int?)null
        };

        if (status is null) return false;

        ctx.Response.StatusCode = status.Value;
        return true;
    }
}


public class ForbiddenAccessException : Exception { }