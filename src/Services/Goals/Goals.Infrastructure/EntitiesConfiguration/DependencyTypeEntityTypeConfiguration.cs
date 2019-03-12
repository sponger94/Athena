using Goals.Domain.AggregatesModel.GoalsAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goals.Infrastructure.EntitiesConfiguration
{
    public class DependencyTypeEntityTypeConfiguration
        : IEntityTypeConfiguration<DependencyType>
    {
        public void Configure(EntityTypeBuilder<DependencyType> dependencyConfig)
        {
            dependencyConfig.ToTable("dependency_types", GoalsContext.DefaultSchema);

            dependencyConfig.HasKey(d => d.Id);

            dependencyConfig.Property(d => d.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            dependencyConfig.Property(d => d.Name)
                .HasMaxLength(32)
                .IsRequired();
        }
    }
}
