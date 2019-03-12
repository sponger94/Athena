using Goals.Domain.AggregatesModel.GoalsAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goals.Infrastructure.EntitiesConfiguration
{
    public class GoalStatusEntityTypeConfiguration
        : IEntityTypeConfiguration<GoalStatus>
    {
        public void Configure(EntityTypeBuilder<GoalStatus> statusConfig)
        {
            statusConfig.ToTable("goal_statuses", GoalsContext.DefaultSchema);

            statusConfig.HasKey(s => s.Id);

            statusConfig.Property(s => s.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            statusConfig.Property(s => s.Name)
                .HasMaxLength(32)
                .IsRequired();
        }
    }
}
