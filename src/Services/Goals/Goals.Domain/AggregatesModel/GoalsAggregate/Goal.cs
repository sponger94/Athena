using Goals.Domain.Events;
using Goals.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace Goals.Domain.AggregatesModel.GoalsAggregate
{
    public class Goal : Entity, IAggregateRoot
    {
        public string IdentityGuid { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public DateTime? DateDue { get; private set; }

        public byte[] Image { get; private set; }

        public GoalSettings GoalSettings { get; private set; }

        public GoalStatus GoalStatus { get; private set; }

        private readonly List<GoalDependency> _dependencies;
        public IReadOnlyCollection<GoalDependency> Dependencies => _dependencies;

        protected Goal()
        {
            _dependencies = new List<GoalDependency>();
            GoalStatus = GoalStatus.InProgress;
        }

        public Goal(string identityGuid, string title, GoalSettings goalSettings,
            string description = null, DateTime? dateDue = null, byte[] image = null) 
            : this()
        {
            IdentityGuid = !string.IsNullOrWhiteSpace(identityGuid) ? identityGuid : throw new ArgumentException("IdentityGuid can not be null or whitespace.");
            Title = !string.IsNullOrWhiteSpace(title) ? title : throw new ArgumentNullException(nameof(title));
            GoalSettings = goalSettings ?? throw new ArgumentNullException(nameof(goalSettings));
            if (dateDue != null && dateDue.Value <= DateTime.Now)
                throw new ArgumentException("The due date of the goal can not be less than or equal to DateTime.Now.");

            DateDue = dateDue;
            Description = description;
            Image = image;

            this.AddDomainEvent(new GoalCreatedDomainEvent(this));
        }

        public void AddDependency(GoalDependency dependency)
        {
            //TODO: Should I bother with the given dependencies Id and this goals id equality?
            _dependencies.Add(dependency);
        }

        public void RemoveDependency(GoalDependency dependency)
        {
            _dependencies.Remove(dependency);
        }

        public void SetTitle(string title)
        {
            Title = title;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetDateDue(DateTime dateDue)
        {
            if (dateDue <= DateTime.Now)
                throw new ArgumentException("The due date of the goal can not be less than or equal to DateTime.Now.");
           
            DateDue = dateDue;
        }

        public void SetCompletedStatus()
        {
            GoalStatus = GoalStatus.Completed;
        }

        public void SetFailedStatus()
        {
            GoalStatus = GoalStatus.Failed;
        }

        public void RemoveGoal()
        {
            this.AddDomainEvent(new GoalRemovedDomainEvent(this));
        }
    }
}
