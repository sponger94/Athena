using System;
using System.Collections.Generic;
using System.Text;
using Goals.Domain.AggregatesModel.GoalsAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tasks.Infrastructure.EntitiesConfiguration
{
    public class GoalSettingsEntityTypeConfiguration
        : IEntityTypeConfiguration<GoalSettings>
    {
        public void Configure(EntityTypeBuilder<GoalSettings> settingsConfig)
        {
            //TODO: Check if necessary
            //settingsConfig.Property<int>("GoalViewAccessibilityId")
            //    .IsRequired();

            //settingsConfig.Property<int>("GoalCommentAccessibilityId")
            //    .IsRequired();

            settingsConfig.HasOne(s => s.GoalViewAccessibility)
                .WithMany()
                .HasForeignKey("GoalViewAccessibilityId")
                .IsRequired();

            settingsConfig.HasOne(s => s.GoalCommentAccessibility)
                .WithMany()
                .HasForeignKey("GoalCommentAccessibilityId")
                .IsRequired();
        }
    }
}
