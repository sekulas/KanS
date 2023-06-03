using KanS.Exceptions;
using System.Text.Json;

namespace KanS.Middleware;

public class ErrorHandlingMiddleware : IMiddleware {
    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {

        try {
            await next.Invoke(context);
        }
        catch(BadRequestException badRequestException) {
            context.Response.StatusCode = 400;
            var errorList = new List<string> { badRequestException.Message };
            var result = JsonSerializer.Serialize(new { errors = errorList });
            await context.Response.WriteAsync(result);
        }
        catch(NotFoundException notFoundException) {
            context.Response.StatusCode = 404;
            var errorList = new List<string> { notFoundException.Message };
            var result = JsonSerializer.Serialize(new { errors = errorList });
            await context.Response.WriteAsync(result);
        }
        catch(UnauthorizedAccessException unauthorizedAccessException) {
            context.Response.StatusCode = 401;
            var errorList = new List<string> { unauthorizedAccessException.Message };
            var result = JsonSerializer.Serialize(new { errors = errorList });
            await context.Response.WriteAsync(result);
        }
        catch(Exception e) {
            context.Response.StatusCode = 500;
            string error = "Internal Server Error: " + e.Message;
            var errorList = new List<string> { error };
            var result = JsonSerializer.Serialize(new { errors = errorList });
            await context.Response.WriteAsync(result);
        }

    }
}
