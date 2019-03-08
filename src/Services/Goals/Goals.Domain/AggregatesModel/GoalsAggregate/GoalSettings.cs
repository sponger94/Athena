using Goals.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace Goals.Domain.AggregatesModel.GoalsAggregate
{
    public class GoalSettings : ValueObject
    {
        public AccessibilityModifier GoalViewAccessibility { get; private set; }
        public AccessibilityModifier GoalCommentAccessibility { get; private set; }

        private GoalSettings() { }

        public GoalSettings(AccessibilityModifier goalViewAccessibility, 
            AccessibilityModifier goalCommentAccessibility)
        {
            GoalViewAccessibility = goalViewAccessibility;
            GoalCommentAccessibility = goalCommentAccessibility;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return GoalViewAccessibility;
            yield return GoalCommentAccessibility;
        }
    }
}
