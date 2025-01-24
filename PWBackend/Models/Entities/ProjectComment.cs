namespace PWBackend.Models.Entities
{
    public class ProjectComment
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Contents { get; set; }
        public required DateTime Time { get; set; } // Time is in UTC!

        public required Project Project { get; set; }
    }
}
