using Goals.Domain.AggregatesModel.GoalsAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tasks.Infrastructure.EntitiesConfiguration
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
                stepConfig.HasForeignKey("DiaryPostId");

                stepConfig.Property(s => s.Name)
                    .HasMaxLength(128)
                    .IsRequired();

                stepConfig.Property(s => s.Description)
                    .IsRequired(false);

                stepConfig.Property(s => s.DueDate)
                    .IsRequired(false);

                stepConfig.HasKey("DiaryPostId", "Name", "Description", "DueDate");
            });
            goalConfig.OwnsMany(g => g.DiaryPosts, postsConfig =>
            {
                postsConfig.ToTable("diary_posts", GoalsContext.DefaultSchema);

                postsConfig.Property(p => p.IdentityGuid)
                    .HasMaxLength(64)
                    .IsRequired();

                postsConfig.Property(p => p.DiaryPostId)
                    .IsRequired();

                postsConfig.Property(p => p.Content)
                    .IsRequired();

                postsConfig.OwnsMany(p => p.Comments, commentsConfig =>
                {
                    commentsConfig.ToTable("comments", GoalsContext.DefaultSchema);

                    commentsConfig.HasKey(c => new { c.IdentityGuid, GoalId = c.DiaryPostId });

                    commentsConfig.Property(c => c.PostedTime)
                        .IsRequired();
                });

                postsConfig.HasKey(p => new { p.IdentityGuid, p.DiaryPostId, p.Content });
            });

            var stepsNavigation = goalConfig.Metadata.FindNavigation(nameof(Goal.Steps));
            stepsNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            var postsNavigation = goalConfig.Metadata.FindNavigation(nameof(Goal.DiaryPosts));
            postsNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
