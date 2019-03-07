using MediatR;

namespace Goals.Domain.Events
{
    public class GoalDeletedDomainEvent : INotification
    {
        public int[] GoalSiblingsHierarchy { get; }

        public GoalDeletedDomainEvent(int[] goalSiblingsHierarchy)
        {
            GoalSiblingsHierarchy = goalSiblingsHierarchy;
        }
    }
}
