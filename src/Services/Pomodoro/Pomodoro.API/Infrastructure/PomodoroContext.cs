﻿using Athena.Pomodoros.API.Infrastructure.EntityConfigurations;
using Athena.Pomodoros.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Athena.Pomodoros.API.Infrastructure
{
    public class PomodoroContext : DbContext
    {
        public DbSet<Pomodoro> Pomodoros { get; set; }

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
