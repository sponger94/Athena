using Goals.Domain.SeedWork;
using System.Collections.Generic;

namespace Goals.Domain.AggregatesModel.GoalsAggregate
{
    public class GoalDependency : ValueObject
    {
        public int GoalId { get; private set; }

        public int DependentOnGoalId { get; private set; }

        private int _dependencyTypeId;
        public DependencyType DependencyType { get; private set; }

        public Goal DependentOnGoal { get; private set; }

        protected GoalDependency()
        { } //TODO: Review if it's needed

        public GoalDependency(int goalId, int dependentOnGoalId, int dependencyTypeId)
        {
            GoalId = goalId;
            DependentOnGoalId = dependentOnGoalId;
            _dependencyTypeId = dependencyTypeId;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return GoalId;
            yield return DependentOnGoalId;
        }
    }
}
