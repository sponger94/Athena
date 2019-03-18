using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Domain.AggregatesModel.TasksAggregate;

namespace Tasks.Infrastructure.EntityConfigurations
{
    public class LabelEntityTypeConfiguration
        : IEntityTypeConfiguration<Label>
    {
        public void Configure(EntityTypeBuilder<Label> labelConfig)
        {
            labelConfig.ToTable("labels", TasksContext.DefaultSchema);

            labelConfig.HasKey(l => l.Id);

            labelConfig.Ignore(l => l.DomainEvents);

            labelConfig.Property(l => l.Id)
                .ForSqlServerUseSequenceHiLo("labelseq", TasksContext.DefaultSchema);

            labelConfig.Property(l => l.Color)
                .IsRequired();

            labelConfig.Property(l => l.Name)
                .HasMaxLength(32)
                .IsRequired();
        }
    }
}
