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
            taskConfig.ToTable("userTasks", TasksContext.DefaultSchema);

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
                .HasDefaultValue(1)
                .IsRequired();

            taskConfig.OwnsMany(t => t.Attachments, attachmentConfig =>
            {
                attachmentConfig.ToTable("attachments", TasksContext.DefaultSchema);

                attachmentConfig.HasForeignKey("UserTaskId");

                attachmentConfig.Property(a => a.Uri)
                    .IsRequired();

                attachmentConfig.HasKey("UserTaskId", "Uri");
            });

            taskConfig.OwnsMany(t => t.LabelItems, labelItemConfig =>
            {
                labelItemConfig.ToTable("labelItems", TasksContext.DefaultSchema);

                labelItemConfig.HasForeignKey("UserTaskId");

                labelItemConfig.Property(li => li.LabelId)
                    .IsRequired();

                labelItemConfig.HasOne(li => li.Label)
                    .WithMany()
                    .HasForeignKey(li => li.LabelId)
                    .OnDelete(DeleteBehavior.Cascade);

                labelItemConfig.HasKey("UserTaskId", "LabelId");
            });
            
            taskConfig.OwnsMany(t => t.Notes, noteConfig =>
            {
                noteConfig.ToTable("notes", TasksContext.DefaultSchema);

                noteConfig.HasForeignKey("UserTaskId");

                noteConfig.Property(a => a.Content)
                    .IsRequired();

                noteConfig.HasKey("UserTaskId", "Content");
            });
            
            taskConfig.OwnsMany(t => t.SubTasks, subTaskConfig =>
            {
                subTaskConfig.ToTable("subTasks", TasksContext.DefaultSchema);

                subTaskConfig.HasForeignKey("UserTaskId");

                subTaskConfig.Property(a => a.Name)
                    .IsRequired();
                
                subTaskConfig.Property(a => a.IsCompleted)
                    .IsRequired();

                subTaskConfig.HasKey("UserTaskId", "Name", "IsCompleted");
            });
        }
    }
}
