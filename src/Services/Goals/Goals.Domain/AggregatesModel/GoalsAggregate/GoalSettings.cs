using System.Collections.Generic;
using Goals.Domain.SeedWork;

namespace Goals.Domain.AggregatesModel.GoalsAggregate
{
    public class GoalSettings 
        : ValueObject
    {
        private int _goalViewAccessibilityId;
        public AccessibilityModifier GoalViewAccessibility { get; private set; }

        private int _goalCommentAccessibilityId;
        public AccessibilityModifier GoalCommentAccessibility { get; private set; }

        /// <summary>
        /// Required by EF
        /// </summary>
        private GoalSettings()
        { }

        public GoalSettings(int goalViewAccessibilityId, int goalCommentAccessibilityId)
        {
            _goalViewAccessibilityId = goalViewAccessibilityId;
            _goalCommentAccessibilityId = goalCommentAccessibilityId;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _goalViewAccessibilityId;
            yield return _goalCommentAccessibilityId;
        }
    }
}
