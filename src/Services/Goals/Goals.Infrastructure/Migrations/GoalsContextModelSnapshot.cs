﻿// <auto-generated />
using System;
using Tasks.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Tasks.Infrastructure.Migrations
{
    [DbContext(typeof(GoalsContext))]
    partial class GoalsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("Relational:Sequence:goal.goalseq", "'goalseq', 'goal', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Goals.Domain.AggregatesModel.GoalsAggregate.AccessibilityModifier", b =>
                {
                    b.Property<int>("Id")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("accessibility_modifiers","goal");
                });

            modelBuilder.Entity("Goals.Domain.AggregatesModel.GoalsAggregate.Goal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "goalseq")
                        .HasAnnotation("SqlServer:HiLoSequenceSchema", "goal")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<DateTime?>("DateDue");

                    b.Property<string>("Description");

                    b.Property<int?>("GoalStatusId");

                    b.Property<string>("IdentityGuid")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<byte[]>("Image");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.HasIndex("GoalStatusId");

                    b.HasIndex("IdentityGuid");

                    b.ToTable("goals","goal");
                });

            modelBuilder.Entity("Goals.Domain.AggregatesModel.GoalsAggregate.GoalStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("goal_statuses","goal");
                });

            modelBuilder.Entity("Goals.Domain.AggregatesModel.GoalsAggregate.Goal", b =>
                {
                    b.HasOne("Goals.Domain.AggregatesModel.GoalsAggregate.GoalStatus", "GoalStatus")
                        .WithMany()
                        .HasForeignKey("GoalStatusId");

                    b.OwnsMany("Goals.Domain.AggregatesModel.GoalsAggregate.DiaryPost", "DiaryPosts", b1 =>
                        {
                            b1.Property<int>("IdentityGuid")
                                .HasMaxLength(64);

                            b1.Property<int>("DiaryPostId");

                            b1.Property<string>("Content");

                            b1.Property<int>("GoalId");

                            b1.Property<DateTime>("PostedTime");

                            b1.HasKey("IdentityGuid", "DiaryPostId", "Content");

                            b1.HasIndex("GoalId");

                            b1.ToTable("diary_posts","goal");

                            b1.HasOne("Goals.Domain.AggregatesModel.GoalsAggregate.Goal")
                                .WithMany("DiaryPosts")
                                .HasForeignKey("GoalId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsMany("Goals.Domain.AggregatesModel.GoalsAggregate.Comment", "Comments", b2 =>
                                {
                                    b2.Property<int>("IdentityGuid");

                                    b2.Property<int>("DiaryPostId");

                                    b2.Property<string>("Content");

                                    b2.Property<string>("DiaryPostContent")
                                        .IsRequired();

                                    b2.Property<int>("DiaryPostId1");

                                    b2.Property<int>("DiaryPostIdentityGuid");

                                    b2.Property<DateTime>("PostedTime");

                                    b2.HasKey("IdentityGuid", "DiaryPostId");

                                    b2.HasIndex("DiaryPostIdentityGuid", "DiaryPostId1", "DiaryPostContent");

                                    b2.ToTable("comments","goal");

                                    b2.HasOne("Goals.Domain.AggregatesModel.GoalsAggregate.DiaryPost")
                                        .WithMany("Comments")
                                        .HasForeignKey("DiaryPostIdentityGuid", "DiaryPostId1", "DiaryPostContent")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });
                        });

                    b.OwnsOne("Goals.Domain.AggregatesModel.GoalsAggregate.GoalSettings", "GoalSettings", b1 =>
                        {
                            b1.Property<int>("GoalId");

                            b1.Property<int>("GoalCommentAccessibilityId");

                            b1.Property<int>("GoalViewAccessibilityId");

                            b1.HasKey("GoalId");

                            b1.HasIndex("GoalCommentAccessibilityId");

                            b1.HasIndex("GoalViewAccessibilityId");

                            b1.ToTable("goals","goal");

                            b1.HasOne("Goals.Domain.AggregatesModel.GoalsAggregate.AccessibilityModifier", "GoalCommentAccessibility")
                                .WithMany()
                                .HasForeignKey("GoalCommentAccessibilityId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.HasOne("Goals.Domain.AggregatesModel.GoalsAggregate.Goal")
                                .WithOne("GoalSettings")
                                .HasForeignKey("Goals.Domain.AggregatesModel.GoalsAggregate.GoalSettings", "GoalId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.HasOne("Goals.Domain.AggregatesModel.GoalsAggregate.AccessibilityModifier", "GoalViewAccessibility")
                                .WithMany()
                                .HasForeignKey("GoalViewAccessibilityId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsMany("Goals.Domain.AggregatesModel.GoalsAggregate.GoalStep", "Steps", b1 =>
                        {
                            b1.Property<int>("DiaryPostId");

                            b1.Property<string>("Name")
                                .HasMaxLength(128);

                            b1.Property<string>("Description");

                            b1.Property<DateTime?>("DueDate");

                            b1.HasKey("DiaryPostId", "Name", "Description", "DueDate");

                            b1.ToTable("GoalStep");

                            b1.HasOne("Goals.Domain.AggregatesModel.GoalsAggregate.Goal")
                                .WithMany("Steps")
                                .HasForeignKey("DiaryPostId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
