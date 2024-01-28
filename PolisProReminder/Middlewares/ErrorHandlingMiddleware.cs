using PolisProReminder.Exceptions;

namespace PolisProReminder.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
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
                case NotFoundException e:
                    context.Response.StatusCode = 404;
                    await context.Response.WriteAsync(e.Message);
                    break;
                case AlreadyExistsException e:
                    context.Response.StatusCode = 409;
                    await context.Response.WriteAsync(e.Message);
                    break;
                case Exception e:
                    _logger.LogError(e, e.Message);

                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("Something went wrong");
                    break;
            }
        }
    }
}
