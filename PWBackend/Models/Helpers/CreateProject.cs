using PWBackend.Models.Entities;

namespace PWBackend.Models.Helpers
{
    public class CreateProject
    {
        public required string Name { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public long DateStarted { get; set; }
        public long DateEnded { get; set; }

        public List<int>? Technologies { get; set; }
        public required int StatusId { get; set; }
        public required int TypeId { get; set; }
    }
}
