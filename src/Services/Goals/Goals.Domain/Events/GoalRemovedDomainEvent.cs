using Goals.Domain.AggregatesModel.GoalsAggregate;
using MediatR;

namespace Goals.Domain.Events
{
    public class GoalRemovedDomainEvent : INotification
    {
        public Goal Goal { get; private set; }

        public GoalRemovedDomainEvent(Goal goal)
        {
            Goal = goal;
        }
    }
}
