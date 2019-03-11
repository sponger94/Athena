using Goals.Domain.AggregatesModel.GoalsAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goals.Infrastructure.EntitiesConfiguration
{
    public class GoalDependencyEntityTypeConfiguration
        : IEntityTypeConfiguration<GoalDependency>
    {
        public void Configure(EntityTypeBuilder<GoalDependency> dependencyConfig)
        {
            dependencyConfig.HasKey(d => new { d.GoalId, d.DependentOnGoalId });
            dependencyConfig.HasIndex(d => new { d.GoalId, d.DependentOnGoalId });

            dependencyConfig.Property(d => d.GoalId)
                .IsRequired();

            dependencyConfig.HasOne(d => d.DependentOnGoal)
                .WithMany();

            dependencyConfig.Property(d => d.DependentOnGoalId)
                .IsRequired();
        }
    }
}
