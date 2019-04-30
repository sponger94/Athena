using Athena.Pomodoro.API.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Athena.Pomodoro.API.Infrastructure
{
    public class PomodoroContext : DbContext
    {
        public DbSet<Model.Pomodoro> Pomodoros { get; set; }

        public PomodoroContext(DbContextOptions<PomodoroContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PomodoroEntityTypeConfiguration());
        }
    }

    public class PomodoroContextDesignFactory : IDesignTimeDbContextFactory<PomodoroContext>
    {
        public PomodoroContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PomodoroContext>()
                .UseSqlServer("Server=.;Initial Catalog=Athena.Services.PomodoroDb;Integrated Security=true");

            return new PomodoroContext(optionsBuilder.Options);
        }
    }
}
