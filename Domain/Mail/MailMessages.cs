namespace PolisProReminder.Domain.Mails;

public static class MailMessages
{
    public static string PolicyCreated => "Polisa nr {0} została pomyślnie zarejestrowana w systmie przez użytkownika {1}";
    public static string PolicyUpdated => "Polisa nr {0} została edytowana przez użytkownika {1}";
}

public static class MailSubjects
{
    public static string PolicyCreated => "[PolisProReminder] Nowa polisa";
    public static string PolicyUpdated => "[PolisProReminder] Polisa zaktualizowana";
}