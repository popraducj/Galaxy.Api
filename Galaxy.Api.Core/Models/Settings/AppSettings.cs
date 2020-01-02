using Galaxy.Api.Core.Models.Settings;

namespace Galaxy.Teams.Core.Models.Settings
{
    public class AppSettings
    {
        public JwtSettings Jwt { get; set; }
        public Urls Urls { get; set; }
        public string AllowedOrigins { get; set; }
    }
}