namespace PWBackend.Models.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public string? ImagePath { get; set; }
        public string? VideoPath { get; set; }
        public DateTime? DateStarted { get; set; }
        public DateTime? DateEnded { get; set; }

        public ICollection<ProjectTechnology>? Technologies { get; set; }
        public ICollection<ProjectTag>? Tags { get; set; }
        public ICollection<ProjectMedia>? Media { get; set; }
        public required ProjectStatus Status { get; set; }
        public required ProjectType Type { get; set; }
        public ICollection<ProjectComment>? Comments { get; set; }
    }
}
