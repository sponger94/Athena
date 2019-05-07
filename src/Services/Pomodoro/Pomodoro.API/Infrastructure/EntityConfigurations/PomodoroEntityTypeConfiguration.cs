using Athena.Pomodoros.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Athena.Pomodoros.API.Infrastructure.EntityConfigurations
{
    public class PomodoroEntityTypeConfiguration : IEntityTypeConfiguration<Pomodoro>
    {
        public void Configure(EntityTypeBuilder<Pomodoro> builder)
        {
            builder.ToTable("pomodoros");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ForSqlServerUseSequenceHiLo("pomodoro_hilo")
                .IsRequired();

            builder.Property(p => p.ProjectId)
                .IsRequired();

            builder.Property(p => p.ProjectName)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(p => p.Duration)
                .IsRequired();

            builder.Property(p => p.Time)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder.Property(p => p.UserId)
                .IsRequired();
        }
    }
}
