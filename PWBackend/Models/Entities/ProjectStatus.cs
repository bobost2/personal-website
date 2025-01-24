namespace PWBackend.Models.Entities
{
    public class ProjectStatus
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? IconPath { get; set; }
        public ICollection<Project>? Projects { get; set; }
    }
}
