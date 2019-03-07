using System;
using System.Collections.Generic;
using System.Text;
using Goals.Domain.AggregatesModel.GoalsAggregate;
using MediatR;

namespace Goals.Domain.Events
{
    public class GoalCreatedDomainEvent : INotification
    {
        public Goal Goal { get;}

        public GoalCreatedDomainEvent(Goal goal)
        {
            Goal = goal;
        }
    }
}
