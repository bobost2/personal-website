namespace PWBackend.Models.Entities
{
    public class ProjectTag
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<Project>? Projects { get; set; }
    }
}
