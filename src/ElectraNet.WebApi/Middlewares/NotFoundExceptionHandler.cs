using ElectraNet.WebApi.Models;
using ElectraNet.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace ElectraNet.WebApi.Middlewares;

public class NotFoundExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        if (exception is not NotFoundException notFoundException)
            return false;

        await httpContext.Response.WriteAsJsonAsync(new Response
        {
            StatusCode = notFoundException.StatusCode,
            Message = notFoundException.Message,
        });

        return true;
    }
}