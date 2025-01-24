namespace PWBackend.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public bool RequiresReset { get; set; } = false;
        public bool HasOTP { get; set; } = false;
        public string? OTPSecret { get; set; }
    }
}
