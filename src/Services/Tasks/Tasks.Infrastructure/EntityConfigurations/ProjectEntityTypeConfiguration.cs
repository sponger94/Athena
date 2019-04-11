using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Domain.AggregatesModel.ProjectsAggregate;

namespace Tasks.Infrastructure.EntityConfigurations
{
    public class ProjectEntityTypeConfiguration
        : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> projectConfig)
        {
            projectConfig.ToTable("projects", TasksContext.DefaultSchema);

            projectConfig.HasKey(p => p.Id);

            projectConfig.Ignore(p => p.DomainEvents);

            projectConfig.Property(p => p.Id)
                .ForSqlServerUseSequenceHiLo("projectsseq", TasksContext.DefaultSchema);

            projectConfig.Property(p => p.Name)
                .HasMaxLength(32)
                .IsRequired();

            projectConfig.Property(p => p.IdentityGuid)
                .IsRequired();

            projectConfig.Property(p => p.Argb)
                .IsRequired();

            projectConfig.Ignore(p => p.Color);
        }
    }
}
