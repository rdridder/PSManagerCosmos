using AspireSample.ProcessApi.Data;
using AspireSample.ProcessApi.Data.Enums;
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

        // Does not seem to work with Cosmos, now set on the model explicitly
        //protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        //{
        //    base.ConfigureConventions(configurationBuilder);
        //    configurationBuilder.Properties<Enum>().HaveConversion<string>();
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultContainer("Process");

            builder.Entity<Process>()
             .ToContainer(nameof(Process))
             .HasPartitionKey(c => c.Id)
             .HasNoDiscriminator()
             .OwnsMany(c => c.Tasks);

    //        builder.Entity<Process>().Property(c => c.Status).HasConversion(
    //v => v.ToString(),
    //v => (Status)Enum.Parse(typeof(Status), v, true));

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
