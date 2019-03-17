using Goals.Domain.AggregatesModel.GoalsAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tasks.Infrastructure.EntitiesConfiguration
{
    public class AccessibilityModifierEntityTypeConfiguration
        : IEntityTypeConfiguration<AccessibilityModifier>
    {
        public void Configure(EntityTypeBuilder<AccessibilityModifier> accessConfig)
        {
            accessConfig.ToTable("accessibility_modifiers", GoalsContext.DefaultSchema);

            accessConfig.HasKey(a => a.Id);

            accessConfig.Property(a => a.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            accessConfig.Property(a => a.Name)
                .HasMaxLength(32)
                .IsRequired();
        }
    }
}
