namespace PWBackend.Models.Helpers
{
    public class PasswordReset
    {
        public required string Username { get; set; }
        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
