using PolisProReminder.Domain.Exceptions;

namespace PolisProReminder.Middlewares;

public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        switch (exception)
        {
            case UnauthorizedException e:
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync(e.Message);
                break;
            case NotAllowedException e:
                context.Response.StatusCode = 405;
                await context.Response.WriteAsync(e.Message);
                break;
            case ForbidException e:
                context.Response.StatusCode = 403;
                break;
            case BadRequestException e:
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(e.Message);
                break;
            case NotFoundException e:
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(e.Message);
                break;
            case AlreadyExistsException e:
                context.Response.StatusCode = 409;
                await context.Response.WriteAsync(e.Message);
                break;
            case UploadAttachmentException e:
                logger.LogError(e, e.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(e.Message);
                break;
            case Exception e:
                logger.LogError(e, e.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Coś poszło nie tak");
                break;
        }
    }
}
