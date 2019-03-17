using System;
using System.Collections.Generic;
using Goals.Domain.Events;
using Goals.Domain.SeedWork;

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

        private List<GoalStep> _steps;
        public IReadOnlyCollection<GoalStep> Steps => _steps;

        private List<DiaryPost> _diaryPosts;
        public IReadOnlyCollection<DiaryPost> DiaryPosts => _diaryPosts;

        protected Goal()
        {
            _steps = new List<GoalStep>();
            _diaryPosts = new List<DiaryPost>();
            GoalStatus = GoalStatus.InProgress;
        }

        public Goal(string identityGuid, string title, GoalSettings goalSettings)
            : this()
        {
            IdentityGuid = !string.IsNullOrWhiteSpace(identityGuid) ? identityGuid : throw new ArgumentException("IdentityGuid can not be null or whitespace.");
            Title = !string.IsNullOrWhiteSpace(title) ? title : throw new ArgumentException(nameof(title));
            GoalSettings = goalSettings ?? throw new ArgumentNullException(nameof(goalSettings));
            this.AddDomainEvent(new GoalCreatedDomainEvent(this));
        }

        public Goal(string identityGuid, string title, GoalSettings goalSettings,
            string description = null, DateTime? dateDue = null, byte[] image = null)
            : this(identityGuid, title, goalSettings)
        {
            if (dateDue != null)
                SetDateDue(dateDue.Value);

            Description = description;
            Image = image;
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
            if (dateDue <= DateTime.Now.AddMinutes(1))
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

        public void AddStep(GoalStep step)
        {
            if (step == null)
                throw new ArgumentNullException(nameof(step));

            if (!_steps.Contains(step))
                _steps.Add(step);
        }

        public void RemoveStep(GoalStep step)
        {
            _steps.Remove(step);
        }

        public void RemoveGoal()
        {
            this.AddDomainEvent(new GoalRemovedDomainEvent(this));
        }
    }
}
