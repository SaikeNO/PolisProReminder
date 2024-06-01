namespace PolisProReminder.Domain.Exceptions;

public class UnauthorizedException(string message) : Exception(message)
{
}
