// Ignore Spelling: Jwt

namespace PolisProReminder
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; } = null!;
        public int JwtExpireHours { get; set; }
        public int RefreshTokenExpireDays { get; set; }

        public string JwtIssuer { get; set; } = null!;
    }
}
