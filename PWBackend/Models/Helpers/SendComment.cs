namespace PWBackend.Models.Helpers
{
    public class SendComment
    {
        public required int ProjectId { get; set; }
        public required string Name { get; set; }
        public required string Comment { get; set; }
    }
}
