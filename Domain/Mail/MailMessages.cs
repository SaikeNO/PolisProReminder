namespace PolisProReminder.Domain.Mail;

public static class MailMessages
{
    public static string PolicyCreated => "Polisa nr {0} została pomyślnie zarejestrowana w systmie przez użytkownika {1}";
    public static string PolicyUpdated => "Polisa nr {0} została edytowana przez użytkownika {1}";
    public static string AssistantCreated => "<p>Witaj {0},</p>" +
            "<p>Twoje konto zostało utworzone. Twój login do platformy PolisProReminder to: <b>{1}</b></p>" +
            "<p>Aby aktywować konto, kliknij poniższy link:</p>" +
            "<a href='{2}'>Aktywuj konto</a>" +
            "<p>Jeśli nie utworzyłeś konta, zignoruj tę wiadomość.</p>";
}

public static class MailSubjects
{
    public static string PolicyCreated => "[PolisProReminder] Nowa polisa";
    public static string PolicyUpdated => "[PolisProReminder] Polisa zaktualizowana";
    public static string AssistantCreated => "[PolisProReminder] Aktywacja konta";
}