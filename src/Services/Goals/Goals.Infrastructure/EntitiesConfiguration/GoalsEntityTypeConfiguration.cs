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
            goalConfig.ToTable("goals", GoalsContext.DefaultSchema);

            goalConfig.HasKey(g => g.Id);

            goalConfig.Ignore(g => g.DomainEvents);

            goalConfig.Property(g => g.Id)
                .ForSqlServerUseSequenceHiLo("goalseq", GoalsContext.DefaultSchema);

            goalConfig.Property(g => g.IdentityGuid)
                .HasMaxLength(200)
                .IsRequired();

            goalConfig.HasIndex("IdentityGuid");

            goalConfig.Property(g => g.Title)
                .HasMaxLength(64)
                .IsRequired();

            goalConfig.OwnsOne(g => g.GoalSettings);
            goalConfig.Property(g => g.Description).IsRequired(false);
            goalConfig.Property(g => g.DateDue).IsRequired(false);
            goalConfig.Property(g => g.Image).IsRequired(false);

            goalConfig.OwnsMany(g => g.Steps, stepConfig =>
            {
                stepConfig.HasForeignKey("GoalId");

                stepConfig.Property(s => s.Name)
                    .HasMaxLength(128)
                    .IsRequired();

                stepConfig.Property(s => s.Description)
                    .IsRequired(false);

                stepConfig.Property(s => s.DueDate)
                    .IsRequired(false);

                stepConfig.HasKey("GoalId", "Name", "Description", "DueDate");
            });

            var navigation = goalConfig.Metadata.FindNavigation(nameof(Goal.Steps));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
