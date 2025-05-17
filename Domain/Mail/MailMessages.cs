namespace PolisProReminder.Domain.Mail;

public static class MailMessages
{
    public static string PolicyCreated => "Polisa nr {0} została pomyślnie zarejestrowana w systmie przez użytkownika {1}";
    public static string PolicyUpdated => "Polisa nr {0} została edytowana przez użytkownika {1}";
    public static string AssistantCreated => "<p>Witaj {0},</p>" +
            "<p>Twoje konto zostało utworzone. Twój login do platformy PolisProReminder to: <b>{1}</b></p>" +
            "<p>Aby aktywować konto i ustawić hasło, kliknij poniższy link:</p>" +
            "<a href='{2}'>Aktywuj konto</a>" +
            "<p>Jeśli nie utworzyłeś konta, zignoruj tę wiadomość.</p>";

    public static string ConfirmEmailChanged => "<p>Witaj {0},</p>" +
            "<p>Aby potwierdzić zmianę adresu e-mail, kliknij w poniższy link:</p>" +
            "<p><a href='{1}'>Potwierdź zmianę adresu e-mail</a></p>" +
            "<p>Jeśli nie zmieniłeś adresu email, zignoruj tę wiadomość.</p>";
}

public static class MailSubjects
{
    public static string PolicyCreated => "[PolisProReminder] Nowa polisa";
    public static string PolicyUpdated => "[PolisProReminder] Polisa zaktualizowana";
    public static string AssistantCreated => "[PolisProReminder] Aktywacja konta";
    public static string ConfirmEmailChanged => "[PolisProReminder] Potwierdzenie zmiany adresu e-mail";
}