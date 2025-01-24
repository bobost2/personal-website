namespace PWBackend.Models.Helpers
{
    public class UserLogin
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public string? OTPCode { get; set; }
    }
}
