using System;
using System.Collections.Generic;
using Goals.Domain.SeedWork;

namespace Goals.Domain.AggregatesModel.GoalsAggregate
{
    public class GoalStep
        : ValueObject
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public DateTime? DueDate { get; private set; }

        private GoalStep()
        { }

        public GoalStep(string name, string description, DateTime? dueDate)
        {
            Name = name;
            Description = description;
            DueDate = dueDate;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return Description;
            yield return DueDate;
        }
    }
}
