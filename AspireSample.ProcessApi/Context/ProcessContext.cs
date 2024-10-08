using AspireSample.ProcessApi.Data;
using Microsoft.EntityFrameworkCore;

namespace AspireSample.ProcessApi.Context
{
    public class ProcessContext : DbContext
    {
        public ProcessContext(DbContextOptions<ProcessContext> options) : base(options)
        {
        }

        public DbSet<Process> Process { get; set; }

        public DbSet<ProcessDefinition> ProcessDefinition { get; set; }

        public DbSet<ProcessTaskDefinition> ProcessTaskDefinition { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultContainer("Process");

            builder.Entity<Process>()
             .ToContainer(nameof(Process))
             .HasPartitionKey(c => c.Id)
             .HasNoDiscriminator()
             .OwnsMany(c => c.Tasks);

            builder.Entity<ProcessTaskDefinition>()
             .ToContainer(nameof(ProcessTaskDefinition))
             .HasPartitionKey(c => c.Id)
             .HasNoDiscriminator();

            builder.Entity<ProcessDefinition>()
             .ToContainer(nameof(ProcessDefinition))
             .HasPartitionKey(c => c.Id)
             .HasNoDiscriminator()
             .OwnsMany(c => c.Tasks);
        }
    }
}
