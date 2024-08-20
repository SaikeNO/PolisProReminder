namespace PolisProReminder.Domain.Constants;

public static class Resources
{
    public const string PeselRegex = "\\d{4}[0-3]{1}\\d{6}";
    public const string PhoneRegex = "(?:\\+48[-\\s]?|0)?\\d{3}[-\\s]?\\d{3}[-\\s]?\\d{3}";
    public const string RegistrationRegex = "([A-Z]{2,3}\\d{4,5}[A-Z]{0,2})";
    public const string PasswordRegex = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$";
}
