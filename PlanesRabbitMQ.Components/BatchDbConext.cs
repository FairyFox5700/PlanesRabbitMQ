using Microsoft.EntityFrameworkCore;

namespace PlanesRabbitMQ.Components
{
    public class BatchDbContext : DbContext
    {
        public BatchDbContext(DbContextOptions<BatchDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BatchStateEntityConfiguration());
            modelBuilder.ApplyConfiguration(new JobStateEntityConfiguration());
        }

        public DbSet<BatchState> BatchStates { get; set; }
        public DbSet<JobState> JobStates { get; set; }
    }
}