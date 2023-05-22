using KanS.Exceptions;
using System.Text.Json;

namespace KanS.Middleware;

public class ErrorHandlingMiddleware : IMiddleware {
    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
        
        try {
            await next.Invoke(context);
        }
        catch (BadRequestException badRequestException) {
            context.Response.StatusCode = 400;
            var result = JsonSerializer.Serialize(new { errors = badRequestException.Message });
            await context.Response.WriteAsync(result);
        }
        catch (NotFoundException notFoundException) {
            context.Response.StatusCode = 404;
            var result = JsonSerializer.Serialize(new { errors = notFoundException.Message });
            await context.Response.WriteAsync(result);
        }
        catch (UnauthorizedAccessException unauthorizedAccessException) {
            context.Response.StatusCode = 401;
            var result = JsonSerializer.Serialize(new { errors = unauthorizedAccessException.Message });
            await context.Response.WriteAsync(result);
        }
        catch (Exception e) {
            context.Response.StatusCode = 500;
            var result = JsonSerializer.Serialize(new { errors = "Something went wrong - " + e.Message });
            await context.Response.WriteAsync(result);
        }

    }
}
