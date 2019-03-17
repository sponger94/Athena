using Goals.Domain.AggregatesModel.GoalsAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tasks.Infrastructure.EntitiesConfiguration
{
    public class LikeEntityTypeConfiguration
        : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> likesConfig)
        {
            likesConfig.ToTable("likes", GoalsContext.DefaultSchema);

            likesConfig.Property(l => l.IdentityGuid)
                .HasMaxLength(32)
                .IsRequired();

            likesConfig.Property(l => l.CommentId)
                .IsRequired(false); 

            likesConfig.Property(l => l.GoalId)
                .IsRequired(false);

            likesConfig.HasKey(l => new object[] { l.IdentityGuid, l.CommentId, l.GoalId });
        }
    }
}
