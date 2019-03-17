using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tasks.Infrastructure.EntitiesConfiguration
{
    //public class GoalDiaryEntityTypeConfiguration
    //    : IEntityTypeConfiguration<GoalDiary>
    //{
    //    public void Configure(EntityTypeBuilder<GoalDiary> diaryConfig)
    //    {
    //        diaryConfig.OwnsMany(d => d.DiaryPosts, postsConfig =>
    //        {
    //            postsConfig.ToTable("diary_posts", GoalsContext.DefaultSchema);

    //            postsConfig.OwnsOne(p => p.PostComment, postCommentConfig =>
    //            {
    //                postCommentConfig.Property(p => p.IdentityGuid)
    //                    .HasMaxLength(64)
    //                    .IsRequired();

    //                postCommentConfig.Property(p => p.DiaryPostId)
    //                    .IsRequired();

    //                postCommentConfig.Property(p => p.Content)
    //                    .IsRequired();

    //                postsConfig.HasKey(p => new object[]
    //                {
    //                    p.PostComment.IdentityGuid, 
    //                    p.PostComment.DiaryPostId, 
    //                    p.PostComment.Content
    //                });
    //            });

    //            postsConfig.OwnsMany(p => p.Comments, commentsConfig =>
    //            {
    //                commentsConfig.ToTable("comments", GoalsContext.DefaultSchema);

    //                commentsConfig.HasKey(c => new object[] { c.IdentityGuid, c.DiaryPostId });

    //                commentsConfig.Property(c => c.PostedTime)
    //                    .IsRequired();
    //            });
    //        });
    //    }
    //}
}
