using PolisProReminder.Domain.Exceptions;
namespace PolisProReminder.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (UnauthorizedException e)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync(e.Message);

            logger.LogWarning(e.Message);
        }
        catch (NotAllowedException e)
        {
            context.Response.StatusCode = 405;
            await context.Response.WriteAsync(e.Message);
        }
        catch (ForbidException e)
        {
            context.Response.StatusCode = 403;

            logger.LogWarning(e.Message);
        }
        catch (BadRequestException e)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(e.Message);
        }
        catch (NotFoundException e)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(e.Message);
        }
        catch (AlreadyExistsException e)
        {
            context.Response.StatusCode = 409;
            await context.Response.WriteAsync(e.Message);
        }
        catch (FileUploadException e)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(e.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something went wrong");
        }
    }
}
