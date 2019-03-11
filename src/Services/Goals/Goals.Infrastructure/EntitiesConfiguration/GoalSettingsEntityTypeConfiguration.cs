using System;
using System.Collections.Generic;
using System.Text;
using Goals.Domain.AggregatesModel.GoalsAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goals.Infrastructure.EntitiesConfiguration
{
    public class GoalSettingsEntityTypeConfiguration
        : IEntityTypeConfiguration<GoalSettings>
    {
        public void Configure(EntityTypeBuilder<GoalSettings> settingsConfig)
        {
            settingsConfig.Property<int>("_goalViewAccessibilityId")
                .IsRequired();

            settingsConfig.Property<int>("_goalCommentAccessibilityId")
                .IsRequired();

            settingsConfig.HasOne(s => s.GoalViewAccessibility)
                .WithMany()
                .HasForeignKey("_goalViewAccessibilityId")
                .IsRequired();

            settingsConfig.HasOne(s => s.GoalCommentAccessibility)
                .WithMany()
                .HasForeignKey("_goalCommentAccessibilityId")
                .IsRequired();
        }
    }
}
