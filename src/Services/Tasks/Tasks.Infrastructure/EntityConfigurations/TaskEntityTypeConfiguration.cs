using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserTask = Tasks.Domain.AggregatesModel.UserTasksAggregate.UserTask;

namespace Tasks.Infrastructure.EntityConfigurations
{
    public class TaskEntityTypeConfiguration
        : IEntityTypeConfiguration<UserTask>
    {
        public void Configure(EntityTypeBuilder<UserTask> taskConfig)
        {
            taskConfig.ToTable("tasks", TasksContext.DefaultSchema);

            taskConfig.HasKey(t => t.Id);

            taskConfig.Ignore(t => t.DomainEvents);

            taskConfig.Property(t => t.Id)
                .ForSqlServerUseSequenceHiLo("taskseq", TasksContext.DefaultSchema);

            taskConfig.Property(t => t.DateCreated)
                .IsRequired();

            taskConfig.Property(t => t.Name)
                .HasMaxLength(32)
                .IsRequired();

            taskConfig.Property(t => t.IsCompleted)
                .IsRequired();

            taskConfig.Property(t => t.ProjectId)
                .IsRequired(false);

            taskConfig.OwnsMany(t => t.Attachments, attachmentConfig =>
            {
                attachmentConfig.HasForeignKey("TaskId");

                attachmentConfig.Property(a => a.Uri)
                    .IsRequired();

                attachmentConfig.HasKey("TaskId", "Uri");
            });

            taskConfig.OwnsMany(t => t.LabelItems, labelItemConfig =>
            {
                labelItemConfig.HasForeignKey("TaskId");

                labelItemConfig.Property(li => li.LabelId)
                    .IsRequired();

                labelItemConfig.HasOne(li => li.Label)
                    .WithMany()
                    .HasForeignKey(li => li.LabelId)
                    .OnDelete(DeleteBehavior.Cascade);

                labelItemConfig.HasKey("TaskId", "LabelId");
            });
            
            taskConfig.OwnsMany(t => t.Notes, noteConfig =>
            {
                noteConfig.HasForeignKey("TaskId");

                noteConfig.Property(a => a.Content)
                    .IsRequired();

                noteConfig.HasKey("TaskId", "Content");
            });
            
            taskConfig.OwnsMany(t => t.SubTasks, subTaskConfig =>
            {
                subTaskConfig.HasForeignKey("TaskId");

                subTaskConfig.Property(a => a.Name)
                    .IsRequired();
                
                subTaskConfig.Property(a => a.IsCompleted)
                    .IsRequired();

                subTaskConfig.HasKey("TaskId", "Name", "IsCompleted");
            });
        }
    }
}
