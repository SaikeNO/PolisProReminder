namespace PolisProReminder.Domain.Exceptions;

public class NotFoundException(string errMessage) : Exception(errMessage)
{
}
