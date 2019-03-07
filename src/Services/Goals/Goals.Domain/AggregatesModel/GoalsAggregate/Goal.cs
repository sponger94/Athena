using Goals.Domain.Events;
using Goals.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace Goals.Domain.AggregatesModel.GoalsAggregate
{
    public class Goal : Entity, IAggregateRoot
    {
        public const string DefaultSchema = "goal";

        public string IdentityGuid { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public DateTime? DateDue { get; private set; }

        public byte[] Image { get; private set; }

        public GoalSettings GoalSettings { get; private set; }

        private readonly List<Goal> _subGoals;
        public IReadOnlyCollection<Goal> SubGoals => _subGoals;

        protected Goal() { }

        public Goal(string identityGuid, string title, string description,
            DateTime dateDue, byte[] image, GoalSettings goalSettings)
        {
            //TODO: Check for null
            IdentityGuid = identityGuid;
            Title = title;
            Description = description;
            DateDue = dateDue;
            Image = image;
            GoalSettings = goalSettings;

            this.AddDomainEvent(new GoalCreatedDomainEvent(this));
        }

        public void AddSubGoal(string identityGuid, string title, string description,
            DateTime dateDue, byte[] image, GoalSettings goalSettings)
        {
            var subGoal = new Goal(identityGuid, title, description, dateDue, image, goalSettings);
            _subGoals.Add(subGoal);
        }
    }
}
