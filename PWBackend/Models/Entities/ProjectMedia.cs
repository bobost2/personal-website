namespace PWBackend.Models.Entities
{
    public class ProjectMedia
    {
        public int Id { get; set; }
        public required string Description { get; set; }
        public bool IsLink { get; set; } = false;
        public required string Path { get; set; }

        public ICollection<Project>? Projects { get; set; }
    }
}
