namespace TallerMecanico.Models
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Rol { get; set; } = string.Empty;

        public string RedirectUrl { get; set; } = string.Empty;
    }
}
