using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Ms06ServicesContracts.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext ctx, Exception ex, CancellationToken ct)
    {
        var (status, title) = ex switch
        {
            ResourceNotFoundException => (StatusCodes.Status404NotFound, "Recurso no encontrado"),
            BusinessRuleException => (StatusCodes.Status400BadRequest, "Regla de negocio incumplida"),
            _ => (StatusCodes.Status500InternalServerError, "Error interno")
        };
        ctx.Response.StatusCode = status;
        await ctx.Response.WriteAsJsonAsync(
            new ProblemDetails { Status = status, Title = title, Detail = ex.Message }, ct);
        return true;
    }
}