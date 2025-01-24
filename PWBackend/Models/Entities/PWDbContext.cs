using Microsoft.EntityFrameworkCore;

namespace PWBackend.Models.Entities
{
    public class PWDbContext : DbContext
    {
        public PWDbContext()
        {
        }

        public PWDbContext(DbContextOptions<PWDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectType> ProjectTypes { get; set; }
        public DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public DbSet<ProjectTechnology> ProjectTechnologies { get; set; }
        public DbSet<ProjectTag> ProjectTags { get; set; }
        public DbSet<ProjectMedia> ProjectMedia { get; set; }
        public DbSet<ProjectComment> ProjectComments { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
