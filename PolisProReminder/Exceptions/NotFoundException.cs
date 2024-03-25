namespace PolisProReminder.Exceptions;

public class NotFoundException(string errMessage) : Exception(errMessage)
{
}
