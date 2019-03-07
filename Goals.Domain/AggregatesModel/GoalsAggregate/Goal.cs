using System;
using System.Collections.Generic;
using Goals.Domain.SeedWork;

namespace Goals.Domain.AggregatesModel.GoalsAggregate
{
    public class Goal : Entity, IAggregateRoot
    {
        public string IdentityGuid { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public DateTime DateDue { get; private set; }

        public byte[] Image { get; private set; }

        public GoalSettings GoalSettings { get; private set; }

        private readonly List<Goal> _subGoals;
        public IReadOnlyCollection<Goal> SubGoals => _subGoals;
    }
}
