using System;
using Goals.Domain.AggregatesModel.GoalsAggregate;
using Goals.Domain.SeedWork;
using System.Collections.Generic;

namespace Goals.Domain.AggregatesModel.UsersAggregate
{
    public class User : Entity, IAggregateRoot
    {
        public string IdentityGuid { get; private set; }

        private readonly List<Goal> _goals;
        public IReadOnlyCollection<Goal> Goals => _goals;
        protected User()
        {
            _goals = new List<Goal>();
        }

        public User(string identityGuid)
            : this()
        {
            IdentityGuid = !string.IsNullOrWhiteSpace(identityGuid) 
                ? identityGuid 
                : throw new ArgumentNullException(nameof(identityGuid));
        }

        public void AddGoal(Goal goal)
        {
            _goals.Add(goal);
        }

        public void RemoveGoal(Goal goal)
        {

        }
    }
}
