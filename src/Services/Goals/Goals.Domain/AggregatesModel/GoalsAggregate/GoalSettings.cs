using Goals.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace Goals.Domain.AggregatesModel.GoalsAggregate
{
    public class GoalSettings : ValueObject
    {
        public ViewOption GoalViewOption { get; private set; }
        public CommentsOption GoalCommentOption { get; private set; }

        private GoalSettings() { }

        public GoalSettings(ViewOption goalViewOption, CommentsOption commentsOption)
        {
            GoalViewOption = goalViewOption;
            GoalCommentOption = commentsOption;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return GoalViewOption;
            yield return GoalCommentOption;
        }
    }
}
