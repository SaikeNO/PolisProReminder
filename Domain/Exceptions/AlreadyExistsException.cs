namespace PolisProReminder.Domain.Exceptions;

public class AlreadyExistsException(string errMessage) : Exception(errMessage)
{
}
