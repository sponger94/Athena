using Goals.Domain.Events;
using Goals.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace Goals.Domain.AggregatesModel.GoalsAggregate
{
    public class Goal : Entity, IAggregateRoot
    {
        public int UserId { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public DateTime? DateDue { get; private set; }

        public byte?[] Image { get; private set; }

        public GoalSettings GoalSettings { get; private set; }

        private readonly List<Goal> _subGoals;
        public IReadOnlyCollection<Goal> SubGoals => _subGoals;

        protected Goal()
        {
            _subGoals = new List<Goal>();
        }

        public Goal(int userId, string title, GoalSettings goalSettings,
            string description = null, DateTime? dateDue = null, byte?[] image = null) : this()
        {
            UserId = userId != 0 ? userId : throw new ArgumentException("UserId can not be equal to 0.");
            Title = !string.IsNullOrWhiteSpace(title) ? title : throw new ArgumentNullException(nameof(title));
            GoalSettings = goalSettings ?? throw new ArgumentNullException(nameof(goalSettings));
            Description = description;
            DateDue = dateDue;
            Image = image;

            this.AddDomainEvent(new GoalCreatedDomainEvent(this));
        }

        public void AddSubGoal(string identityGuid, string title, GoalSettings goalSettings,
            string description = null, DateTime? dateDue = null, byte?[] image = null)
        {
            var subGoal = new Goal(identityGuid, title, goalSettings, description, dateDue, image);
            _subGoals.Add(subGoal);
        }
    }
}
