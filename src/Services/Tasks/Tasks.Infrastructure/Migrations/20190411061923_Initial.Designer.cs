﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tasks.Infrastructure;

namespace Tasks.Infrastructure.Migrations
{
    [DbContext(typeof(TasksContext))]
    [Migration("20190411061923_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("Relational:Sequence:tasks.labelseq", "'labelseq', 'tasks', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("Relational:Sequence:tasks.projectsseq", "'projectsseq', 'tasks', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("Relational:Sequence:tasks.taskseq", "'taskseq', 'tasks', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Tasks.Domain.AggregatesModel.ProjectsAggregate.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "projectsseq")
                        .HasAnnotation("SqlServer:HiLoSequenceSchema", "tasks")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<int>("Argb");

                    b.Property<string>("IdentityGuid")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("projects","tasks");
                });

            modelBuilder.Entity("Tasks.Domain.AggregatesModel.UserTasksAggregate.Label", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "labelseq")
                        .HasAnnotation("SqlServer:HiLoSequenceSchema", "tasks")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<int>("Argb");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("labels","tasks");
                });

            modelBuilder.Entity("Tasks.Domain.AggregatesModel.UserTasksAggregate.UserTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "taskseq")
                        .HasAnnotation("SqlServer:HiLoSequenceSchema", "tasks")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<DateTime>("DateCreated");

                    b.Property<bool>("IsCompleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<int?>("ProjectId");

                    b.HasKey("Id");

                    b.ToTable("userTasks","tasks");
                });

            modelBuilder.Entity("Tasks.Domain.AggregatesModel.UserTasksAggregate.UserTask", b =>
                {
                    b.OwnsMany("Tasks.Domain.AggregatesModel.UserTasksAggregate.Attachment", "Attachments", b1 =>
                        {
                            b1.Property<int>("UserTaskId");

                            b1.Property<string>("Uri");

                            b1.HasKey("UserTaskId", "Uri");

                            b1.ToTable("attachments","tasks");

                            b1.HasOne("Tasks.Domain.AggregatesModel.UserTasksAggregate.UserTask")
                                .WithMany("Attachments")
                                .HasForeignKey("UserTaskId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsMany("Tasks.Domain.AggregatesModel.UserTasksAggregate.Note", "Notes", b1 =>
                        {
                            b1.Property<int>("UserTaskId");

                            b1.Property<string>("Content");

                            b1.HasKey("UserTaskId", "Content");

                            b1.ToTable("notes","tasks");

                            b1.HasOne("Tasks.Domain.AggregatesModel.UserTasksAggregate.UserTask")
                                .WithMany("Notes")
                                .HasForeignKey("UserTaskId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsMany("Tasks.Domain.AggregatesModel.UserTasksAggregate.SubTask", "SubTasks", b1 =>
                        {
                            b1.Property<int>("UserTaskId");

                            b1.Property<string>("Name");

                            b1.Property<bool>("IsCompleted");

                            b1.HasKey("UserTaskId", "Name", "IsCompleted");

                            b1.ToTable("subTasks","tasks");

                            b1.HasOne("Tasks.Domain.AggregatesModel.UserTasksAggregate.UserTask")
                                .WithMany("SubTasks")
                                .HasForeignKey("UserTaskId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsMany("Tasks.Domain.AggregatesModel.UserTasksAggregate.UserTaskLabelItem", "LabelItems", b1 =>
                        {
                            b1.Property<int>("UserTaskId");

                            b1.Property<int>("LabelId");

                            b1.HasKey("UserTaskId", "LabelId");

                            b1.HasIndex("LabelId");

                            b1.ToTable("labelItems","tasks");

                            b1.HasOne("Tasks.Domain.AggregatesModel.UserTasksAggregate.Label", "Label")
                                .WithMany()
                                .HasForeignKey("LabelId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.HasOne("Tasks.Domain.AggregatesModel.UserTasksAggregate.UserTask")
                                .WithMany("LabelItems")
                                .HasForeignKey("UserTaskId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}