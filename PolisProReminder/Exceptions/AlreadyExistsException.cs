namespace PolisProReminder.Exceptions;

public class AlreadyExistsException(string errMessage) : Exception(errMessage)
{
}
