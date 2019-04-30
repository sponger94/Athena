using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Athena.Pomodoro.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Athena.Pomodoro.API.Infrastructure.EntityConfigurations
{
    public class PomodoroEntityTypeConfiguration : IEntityTypeConfiguration<Model.Pomodoro>
    {
        public void Configure(EntityTypeBuilder<Model.Pomodoro> builder)
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
