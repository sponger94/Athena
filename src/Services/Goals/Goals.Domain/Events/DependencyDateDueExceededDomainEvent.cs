using Goals.Domain.AggregatesModel.GoalsAggregate;
using MediatR;

namespace Goals.Domain.Events
{
    public class DependencyDateDueExceededDomainEvent
        : INotification
    {
        public Goal Goal { get; }

        public GoalDependency GoalDependency { get;}

        public DependencyDateDueExceededDomainEvent(Goal goal, GoalDependency goalDependency)
        {
            Goal = goal;
            GoalDependency = goalDependency;
        }
    }
}
