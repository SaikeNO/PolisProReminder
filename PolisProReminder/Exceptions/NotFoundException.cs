namespace PolisProReminder.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string errMessage) : base(errMessage)
        { }
    }
}
