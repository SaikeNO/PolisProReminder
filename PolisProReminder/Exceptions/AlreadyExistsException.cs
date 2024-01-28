namespace PolisProReminder.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string errMessage) : base(errMessage)
        {
        }
    }
}
