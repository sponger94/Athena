using Goals.Domain.AggregatesModel.GoalsAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goals.Infrastructure.EntitiesConfiguration
{
    public class GoalsEntityTypeConfiguration
        : IEntityTypeConfiguration<Goal>
    {
        public void Configure(EntityTypeBuilder<Goal> goalConfig)
        {
            goalConfig.ToTable("goals", Goal.DefaultSchema);

            goalConfig.HasKey(g => g.Id);

            goalConfig.Ignore(g => g.DomainEvents);

            goalConfig.Property(g => g.Id)
                .ForSqlServerUseSequenceHiLo("goalseq", Goal.DefaultSchema);

            goalConfig.Property(g => g.IdentityGuid)
                .HasMaxLength(200)
                .IsRequired();

            goalConfig.HasIndex("IdentityGuid");

            goalConfig.Property(g => g.Title)
                .HasMaxLength(64)
                .IsRequired(true);

            goalConfig.OwnsOne<GoalSettings>(g => g.GoalSettings);
            goalConfig.Property(g => g.Description).IsRequired(false);
            goalConfig.Property(g => g.DateDue).IsRequired(false);
            goalConfig.Property(g => g.Image).IsRequired(false);

            var navigation = goalConfig.Metadata.FindNavigation(nameof(Goal.SubGoals));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
