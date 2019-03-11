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

            goalConfig.Property(g => g.UserId)
                .HasMaxLength(200)
                .IsRequired();

            goalConfig.HasIndex("UserId");

            goalConfig.Property(g => g.Title)
                .HasMaxLength(64)
                .IsRequired();

            goalConfig.OwnsOne(g => g.GoalSettings);
            goalConfig.Property(g => g.Description).IsRequired(false);
            goalConfig.Property(g => g.DateDue).IsRequired(false);
            goalConfig.Property(g => g.Image).IsRequired(false);

            goalConfig.HasMany(g => g.Dependencies)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            var navigation = goalConfig.Metadata.FindNavigation(nameof(Goal.Dependencies));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
