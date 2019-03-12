using Goals.Domain.AggregatesModel.GoalsAggregate;
using MediatR;

namespace Goals.Domain.Events
{
    public class GoalStepDateDueExceededDomainEvent
        : INotification
    {
        public Goal Goal { get; }

        public GoalStep GoalStep { get;}

        public GoalStepDateDueExceededDomainEvent(Goal goal, GoalStep goalStep)
        {
            Goal = goal;
            GoalStep = goalStep;
        }
    }
}
